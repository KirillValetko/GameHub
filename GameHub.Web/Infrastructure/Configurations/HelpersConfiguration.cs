using GameHub.Common.Helpers;
using GameHub.Common.Helpers.Interfaces;
using GameHub.DAL.Models;

namespace GameHub.Web.Infrastructure.Configurations
{
    public static class HelpersConfiguration
    {
        public static void InitHelpers(this IServiceCollection services)
        {
            services.AddScoped<IPaginationHelper<User>, PaginationHelper<User>>();
            services.AddScoped<IPaginationHelper<Game>, PaginationHelper<Game>>();
            services.AddScoped<IPaginationHelper<GameDifficulty>, PaginationHelper<GameDifficulty>>();
            services.AddScoped<IPaginationHelper<UserGameStats>, PaginationHelper<UserGameStats>>();
        }
    }
}
