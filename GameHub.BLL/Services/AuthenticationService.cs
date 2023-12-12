using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.Common.Constants;
using GameHub.Common.Providers.Interfaces;
using GameHub.DAL.Filters;
using GameHub.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GameHub.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashProvider _hashProvider;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IUserRepository userRepository,
            IHashProvider hashProvider,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _hashProvider = hashProvider;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(LoginModel item)
        {
            var dbItem = await _userRepository.GetByFilterAsync(
                new UserFilter { Login = item.Login });

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.InvalidCredentials);
            }

            var salt = dbItem.Password.Substring(dbItem.Password.LastIndexOf(SaltConstants.Separator) + 1);
            var hash = _hashProvider.GetHash(item.Password, salt);

            if (!dbItem.Password.Equals(hash))
            {
                throw new Exception(ExceptionMessageConstants.InvalidCredentials);
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, dbItem.Id),
                new (ClaimTypes.Role, dbItem.Role)
            };

            var jwt = new JwtSecurityToken(
                issuer: JwtOptionsConstants.IssuerOpt,
                audience: JwtOptionsConstants.AudienceOpt,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMonths(15),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[JwtOptionsConstants.Key])),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
