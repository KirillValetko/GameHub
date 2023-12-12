using GameHub.DAL.Models;

namespace GameHub.DAL.Infrastructure.DbSettings
{
    public class BaseGameHubDbSettings<TDbModel>
        where TDbModel : BaseDbModel
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
