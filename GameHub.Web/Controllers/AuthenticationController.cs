using AutoMapper;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.Web.Models.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        
        public AuthenticationController(IAuthenticationService authenticationService,
            IMapper mapper,
            ILogger<AuthenticationController> logger) : base(mapper, logger)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public Task<IActionResult> PostAsync(LoginDto item)
        {
            var mappedItem = _mapper.Map<LoginModel>(item);

            return ProcessRequestAsync<string>(() => _authenticationService.AuthenticateAsync(mappedItem));
        }
    }
}
