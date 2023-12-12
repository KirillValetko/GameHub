﻿using GameHub.BLL.Services;
using GameHub.BLL.Services.Interfaces;

namespace GameHub.Web.Infrastructure.Configurations
{
    public static class ServiceConfiguration
    {
        public static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
