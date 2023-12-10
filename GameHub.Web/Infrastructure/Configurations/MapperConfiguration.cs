using GameHub.BLL.Infrastructure;
using GameHub.DAL.Infrastructure;

namespace GameHub.Web.Infrastructure.Configurations
{
    public static class MapperConfiguration
    {
        public static void InitMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DalMapperProfile), typeof(BllMapperProfile), typeof(ApiMapperProfile));
        }
    }
}
