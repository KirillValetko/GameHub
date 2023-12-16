using AutoMapper;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.Web.Models.DtoModels;
using GameHub.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GameHub.Common.Constants;
using GameHub.Common.Models;
using GameHub.DAL.Filters;
using GameHub.Web.Infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;

namespace GameHub.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService,
            IMapper mapper,
            ILogger<UserController> logger) : base(mapper, logger)
        {
            _userService = userService;
        }

        [HttpGet("Profile")]
        public Task<IActionResult> GetAsync()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var id = string.Empty;

            if (identity != null)
            {
                var claims = identity.Claims;
                id = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            }

            return ProcessRequestAsync<UserModel, UserViewModel>(() =>
                _userService.GetByFilterAsync(new UserFilter { Id = id }));
        }

        [HttpGet("All")]
        [RequiresRoleClaim(RoleConstants.Admin)]
        public Task<IActionResult> GetAsync([FromQuery] PaginationRequest<UserFilter> request)
        {
            return ProcessRequestAsync<PaginationResponse<UserModel>, PaginationResponse<UserViewModel>>(() =>
                _userService.GetPaginatedAsync(request));
        }

        [HttpPost]
        [AllowAnonymous]
        public Task<IActionResult> PostAsync(UserDto item)
        {
            var mappedItem = _mapper.Map<UserModel>(item);

            return ProcessRequestAsync<UserViewModel>(() => _userService.CreateAsync(mappedItem));
        }

        [HttpPut]
        public Task<IActionResult> PutAsync(UserDto item)
        {
            var mappedItem = _mapper.Map<UserModel>(item);

            return ProcessRequestAsync<UserViewModel>(() => _userService.UpdateAsync(mappedItem));
        }

        [HttpDelete("{id}")]
        [RequiresRoleClaim(RoleConstants.Admin)]
        public Task<IActionResult> DeleteAsync(string id)
        {
            return ProcessRequestAsync<UserViewModel>(() => _userService.DeleteAsync(id));
        }
    }
}
