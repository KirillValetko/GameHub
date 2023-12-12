using AutoMapper;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.Web.Models.DtoModels;
using GameHub.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService,
            IMapper mapper,
            ILogger<BaseController> logger) : base(mapper, logger)
        {
            _userService = userService;
        }

        [HttpPost]
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
    }
}
