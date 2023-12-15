using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Models;

namespace GameHub.DAL.Repositories.Interfaces
{
    public interface IGameDifficultyRepository : IBaseRepository<GameDifficulty, GameDifficultyDataModel, GameDifficultyFilter>
    {
    }
}
