using AutoMapper;
using GameHub.Common.Constants;
using GameHub.DAL.DataModels;
using GameHub.DAL.Models;

namespace GameHub.DAL.Infrastructure.MapperValueResolvers
{
    public class UserValueResolver : IValueResolver<User, UserDataModel, string>
    {
        public string Resolve(User source, UserDataModel destination, string destMember, ResolutionContext context)
        {
            var role = source.Role switch
            {
                0 => RoleConstants.User,
                1 => RoleConstants.Admin,
                _ => string.Empty
            };

            return role;
        }
    }
}
