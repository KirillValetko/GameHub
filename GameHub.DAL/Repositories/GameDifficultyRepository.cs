using AutoMapper;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Infrastructure.DbSettings;
using GameHub.DAL.Models;
using GameHub.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Linq;

namespace GameHub.DAL.Repositories
{
    public class GameDifficultyRepository :
        BaseRepository<GameDifficulty, GameDifficultyDataModel, GameDifficultyFilter>,
        IGameDifficultyRepository
    {

        public GameDifficultyRepository(IMapper mapper,
            IOptions<GameDifficultiesCollectionSettings> settings) : base(mapper, settings)
        {
        }

        protected override IMongoQueryable<GameDifficulty> AddFilterConditions(IMongoQueryable<GameDifficulty> source,
            GameDifficultyFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.GameId))
            {
                source = source.Where(gd => gd.GameId.Equals(filter.GameId));
            }

            return source;
        }
    }
}
