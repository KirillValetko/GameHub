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
    public class GameRepository :
        BaseRepository<Game, GameDataModel, GameFilter>,
        IGameRepository
    {
        public GameRepository(IPaginationHelper<Game> paginationHelper,
            IMapper mapper,
            IOptions<GamesCollectionSettings> settings) : base(paginationHelper, mapper, settings)
        {
        }

        protected override IMongoQueryable<Game> AddFilterConditions(IMongoQueryable<Game> source, GameFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.GameName))
            {
                source = source.Where(g => g.GameName.Contains(filter.GameName));
            }

            return source;
        }
    }
}
