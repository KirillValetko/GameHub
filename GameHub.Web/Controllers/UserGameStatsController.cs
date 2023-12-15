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
    public class UserGameStatsController : BaseController
    {
        private readonly IUserGameStatsService _userGameStatsService;

        public UserGameStatsController(IUserGameStatsService userGameStatsService,
            IMapper mapper,
            ILogger<UserGameStatsController> logger) : base(mapper, logger)
        {
            _userGameStatsService = userGameStatsService;
        }

        [HttpPost]
        public Task<IActionResult> PostAsync(UserGameStatsDto item)
        {
            var mappedItem = _mapper.Map<UserGameStatsModel>(item);

            return ProcessRequestAsync<UserGameStatsViewModel>(() =>
                _userGameStatsService.CreateAsync(mappedItem));
        }
    }
}
