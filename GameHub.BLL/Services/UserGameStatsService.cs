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
    public class UserGameStatsService :
        BaseService<UserGameStats, UserGameStatsDataModel, UserGameStatsModel, UserGameStatsFilter>,
        IUserGameStatsService
    {
        private readonly IGameDifficultyRepository _gameDifficultyRepository;
        private readonly IUserRepository _userRepository;

        public UserGameStatsService(IUserGameStatsRepository repository,
            IGameDifficultyRepository gameDifficultyRepository,
            IUserRepository userRepository,
            IMapper mapper) : base(repository, mapper)
        {
            _gameDifficultyRepository = gameDifficultyRepository;
            _userRepository = userRepository;
        }

        public override async Task CreateAsync(UserGameStatsModel item)
        {
            var gameDifficulty = await _gameDifficultyRepository.GetByFilterAsync(
                new GameDifficultyFilter{Id = item.DifficultyId});

            if (gameDifficulty == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            if (string.IsNullOrEmpty(item.UserId))
            {
                await base.CreateAsync(item);
            }
            else
            {
                var user = await _userRepository.GetByFilterAsync(new UserFilter { Id = item.UserId });

                if (user == null)
                {
                    throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
                }

                var userGameStats = await _repository.GetByFilterAsync(
                    new UserGameStatsFilter { DifficultyId = item.DifficultyId, UserId = item.UserId });

                if (userGameStats == null)
                {
                    await base.CreateAsync(item);
                }
                else
                {
                    userGameStats.NumberOfGames++;

                    if (!item.BestTime.Equals(TimeSpan.Zero))
                    {
                        userGameStats.NumberOfWins++;

                        if (userGameStats.BestTime > item.BestTime)
                        {
                            userGameStats.BestTime = item.BestTime;
                        }
                    }
                    
                    await _repository.UpdateAsync(userGameStats);
                }
            }
        }
    }
}
