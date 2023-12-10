using GameHub.Common.Constants;
using Microsoft.OpenApi.Models;

namespace GameHub.Web.Infrastructure.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void InitSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(SwaggerOptionsConstants.Version, new OpenApiInfo
                {
                    Version = SwaggerOptionsConstants.Version,
                    Title = SwaggerOptionsConstants.Title,
                    Description = SwaggerOptionsConstants.Description
                });
                opt.AddSecurityDefinition(SwaggerOptionsConstants.SecurityScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = SwaggerOptionsConstants.DefinitionName,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = SwaggerOptionsConstants.SecurityScheme,
                    BearerFormat = SwaggerOptionsConstants.BearerFormat
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = SwaggerOptionsConstants.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
