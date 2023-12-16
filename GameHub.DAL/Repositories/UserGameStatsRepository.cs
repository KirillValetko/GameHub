using AutoMapper;
using GameHub.Common.Helpers.Interfaces;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Infrastructure.DbSettings;
using GameHub.DAL.Models;
using GameHub.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;

namespace GameHub.DAL.Repositories
{
    public class UserGameStatsRepository :
        BaseRepository<UserGameStats, UserGameStatsDataModel, UserGameStatsFilter>,
        IUserGameStatsRepository
    {
        public UserGameStatsRepository(IPaginationHelper<UserGameStats> paginationHelper,
            IMapper mapper,
            IOptions<UserGameStatsCollectionSettings> settings) : base(paginationHelper, mapper, settings)
        {
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
            if (filter.IsMinTime.HasValue && filter.IsMinTime.Value)
            {
                source = source.OrderBy(ugs => ugs.BestTime);
            }

            if (!string.IsNullOrEmpty(filter.GameId))
            {
                source = source.Where(ugs => ugs.GameId.Equals(filter.GameId));
            }

            if (!string.IsNullOrEmpty(filter.DifficultyId))
            {
                source = source.Where(ugs => ugs.DifficultyId.Equals(filter.DifficultyId));
            }

            if (!string.IsNullOrEmpty(filter.UserId))
            {
                source = source.Where(ugs => ugs.UserId.Equals(filter.UserId));
            }

            return source;
        }
    }
}
