using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Models;

namespace GameHub.DAL.Repositories.Interfaces
{
    public interface IUserGameStatsRepository : IBaseRepository<UserGameStats, UserGameStatsDataModel, UserGameStatsFilter>
    {
    }
}
