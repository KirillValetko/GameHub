using GameHub.Common.Constants;
using GameHub.DAL.Infrastructure.DbSettings;

namespace GameHub.Web.Infrastructure.Configurations
{
    public static class SettingsConfiguration
    {
        public static void InitSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<UsersCollectionSettings>(opt =>
            {
                opt.ConnectionString = configuration.GetSection(DbSettingsConstants.ConnectionString).Value;
                opt.DatabaseName = configuration.GetSection(DbSettingsConstants.DatabaseName).Value;
                opt.CollectionName = configuration.GetSection(DbSettingsConstants.UsersCollection).Value;
            });
            services.Configure<GamesCollectionSettings>(opt =>
            {
                opt.ConnectionString = configuration.GetSection(DbSettingsConstants.ConnectionString).Value;
                opt.DatabaseName = configuration.GetSection(DbSettingsConstants.DatabaseName).Value;
                opt.CollectionName = configuration.GetSection(DbSettingsConstants.GamesCollection).Value;
            });
            services.Configure<GameDifficultiesCollectionSettings>(opt =>
            {
                opt.ConnectionString = configuration.GetSection(DbSettingsConstants.ConnectionString).Value;
                opt.DatabaseName = configuration.GetSection(DbSettingsConstants.DatabaseName).Value;
                opt.CollectionName = configuration.GetSection(DbSettingsConstants.GameDifficultiesCollection).Value;
            });
        }
    }
}
