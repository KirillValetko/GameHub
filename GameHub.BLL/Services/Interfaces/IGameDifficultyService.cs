using GameHub.BLL.Models;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Models;

namespace GameHub.BLL.Services.Interfaces
{
    public interface IGameDifficultyService : 
        IBaseService<GameDifficulty, GameDifficultyDataModel, GameDifficultyModel, GameDifficultyFilter>
    {
    }
}
