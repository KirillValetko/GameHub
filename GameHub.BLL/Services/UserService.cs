using AutoMapper;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.Common.Constants;
using GameHub.Common.Providers.Interfaces;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Models;
using GameHub.DAL.Repositories.Interfaces;

namespace GameHub.BLL.Services
{
    public class UserService :
        BaseService<User, UserDataModel, UserModel, UserFilter>,
        IUserService
    {
        private readonly IHashProvider _hashProvider;
        private readonly ISaltProvider _saltProvider;

        public UserService(IUserRepository repository,
            IHashProvider hashProvider,
            ISaltProvider saltProvider,
            IMapper mapper) : base(repository, mapper)
        {
            _hashProvider = hashProvider;
            _saltProvider = saltProvider;
        }

        public override async Task CreateAsync(UserModel item)
        {
            var dbItem = await _repository.GetByFilterAsync(new UserFilter { Login = item.Login });

            if (dbItem != null)
            {
                throw new Exception(ExceptionMessageConstants.UsedEmail);
            }

            item.UserName = UserConstants.User + Guid.NewGuid().ToString()[..8];
            var salt = _saltProvider.GetSalt();
            var passwordHash = _hashProvider.GetHash(item.Password, salt);
            item.Password = passwordHash;
            await base.CreateAsync(item);
        }

        public override async Task UpdateAsync(UserModel item)
        {
            var dbItem = await _repository.GetByFilterAsync(new UserFilter { Id = item.Id });

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            if (!string.IsNullOrEmpty(item.UserName))
            {
                var itemWithUsername = await _repository.GetByFilterAsync(
                    new UserFilter { UserName = item.UserName });

                if (itemWithUsername != null)
                {
                    throw new Exception(ExceptionMessageConstants.UsedUserName);
                }
            }

            if (!string.IsNullOrEmpty(item.Password))
            {
                var salt = _saltProvider.GetSalt();
                var passwordHash = _hashProvider.GetHash(item.Password, salt);
                item.Password = passwordHash;
            }

            await base.UpdateAsync(item);
        }
    }
}
