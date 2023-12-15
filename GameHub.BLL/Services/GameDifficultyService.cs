using AutoMapper;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.Common.Constants;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Models;
using GameHub.DAL.Repositories.Interfaces;

namespace GameHub.BLL.Services
{
    public class GameDifficultyService :
        BaseService<GameDifficulty, GameDifficultyDataModel, GameDifficultyModel, GameDifficultyFilter>,
        IGameDifficultyService
    {
        private readonly IGameRepository _gameRepository;

        public GameDifficultyService(IGameDifficultyRepository repository,
            IGameRepository gameRepository,
            IMapper mapper) : base(repository, mapper)
        {
            _gameRepository = gameRepository;
        }

        public override async Task CreateAsync(GameDifficultyModel item)
        {
            var game = await _gameRepository.GetByFilterAsync(new GameFilter { Id = item.GameId });

            if (game == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            await base.CreateAsync(item);
        }

        public override async Task UpdateAsync(GameDifficultyModel item)
        {
            var dbItem = await _repository.GetByFilterAsync(new GameDifficultyFilter { Id = item.Id });

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            if (!item.GameId.Equals(dbItem.GameId))
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            await base.UpdateAsync(item);
        }
    }
}
