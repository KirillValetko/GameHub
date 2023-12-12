using GameHub.DAL.Repositories;
using GameHub.DAL.Repositories.Interfaces;

namespace GameHub.Web.Infrastructure.Configurations
{
    public static class RepositoryConfiguration
    {
        public static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
