using System.Text;
using GameHub.Common.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GameHub.Web.Infrastructure.Configurations
{
    public static class JwtConfiguration
    {
        public static void InitJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtOptionsConstants.IssuerOpt,

                    ValidateAudience = true,
                    ValidAudience = JwtOptionsConstants.AudienceOpt,

                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[JwtOptionsConstants.Key])),
                    ValidateIssuerSigningKey = true
                };
            });
        }
    }
}
