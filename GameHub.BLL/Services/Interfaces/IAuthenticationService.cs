using GameHub.BLL.Models;

namespace GameHub.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateAsync(LoginModel item);
    }
}
