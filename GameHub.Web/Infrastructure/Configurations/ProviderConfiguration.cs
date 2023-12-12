using GameHub.Common.Providers;
using GameHub.Common.Providers.Interfaces;

namespace GameHub.Web.Infrastructure.Configurations
{
    public static class ProviderConfiguration
    {
        public static void InitProviders(this IServiceCollection services)
        {
            services.AddScoped<ISaltProvider, SaltProvider>();
            services.AddScoped<IHashProvider, HashProvider>();
        }
    }
}
