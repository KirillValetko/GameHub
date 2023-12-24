using AutoMapper;
using GameHub.Common.Helpers.Interfaces;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Infrastructure.DbSettings;
using GameHub.DAL.Models;
using GameHub.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GameHub.DAL.Repositories
{
    public class UserGameStatsRepository :
        BaseRepository<UserGameStats, UserGameStatsDataModel, UserGameStatsFilter>,
        IUserGameStatsRepository
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserGameStatsRepository(IPaginationHelper<UserGameStats> paginationHelper,
            IMapper mapper,
            IOptions<UserGameStatsCollectionSettings> settings) : base(paginationHelper, mapper, settings)
        {
            _usersCollection = _collection.Database.GetCollection<User>(settings.Value.UsersCollectionName);
        }

        protected override void PrepareForCreation(UserGameStats item)
        {
            base.PrepareForCreation(item);
            item.NumberOfGames = 1;

            if (!item.BestTime.Equals(TimeSpan.Zero))
            {
                item.NumberOfWins = 1;
            }
        }

        protected override IMongoQueryable<UserGameStats> AddFilterConditions(
            IMongoQueryable<UserGameStats> source, UserGameStatsFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.DifficultyId))
            {
                source = source.Where(ugs => ugs.DifficultyId.Equals(filter.DifficultyId));
            }

            if (!string.IsNullOrEmpty(filter.UserId))
            {
                source = source.Where(ugs => ugs.UserId.Equals(filter.UserId));
            }

            if (filter.IncludeUser.HasValue && filter.IncludeUser.Value)
            {
                source = source
                    .GroupJoin(_usersCollection,
                        userGameStats => userGameStats.UserId,
                        user => user.Id,
                        (userGameStats, users) => new UserGameStats
                        {
                            Id = userGameStats.Id,
                            NumberOfGames = userGameStats.NumberOfGames,
                            NumberOfWins = userGameStats.NumberOfWins,
                            BestTime = userGameStats.BestTime,
                            DifficultyId = userGameStats.DifficultyId,
                            UserId = userGameStats.UserId,
                            User = users.First()
                        });
            }

            source = source
                .Where(ugs => !string.IsNullOrEmpty(ugs.UserId))
                .OrderBy(ugs => ugs.BestTime);

            return source;
        }
    }
}
