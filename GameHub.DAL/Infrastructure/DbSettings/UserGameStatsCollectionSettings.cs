using GameHub.DAL.Models;

namespace GameHub.DAL.Infrastructure.DbSettings
{
    public class UserGameStatsCollectionSettings : BaseGameHubDbSettings<UserGameStats>
    {
        public string UsersCollectionName { get; set; } 
    }
}
