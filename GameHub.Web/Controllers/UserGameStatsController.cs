using AutoMapper;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.Common.Models;
using GameHub.DAL.Filters;
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

        [HttpGet("Top")]
        public Task<IActionResult> GetAsync([FromQuery] PaginationRequest<UserGameStatsFilter> request)
        {
            return ProcessRequestAsync<PaginationResponse<UserGameStatsModel>, PaginationResponse<UserGameStatsViewModel>>(
                () => _userGameStatsService.GetPaginatedAsync(request));
        }

        [HttpGet]
        public Task<IActionResult> GetAsync([FromQuery] UserGameStatsFilter filter)
        {
            return ProcessRequestAsync<UserGameStatsModel, UserGameStatsViewModel>(() =>
                _userGameStatsService.GetByFilterAsync(filter));
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
