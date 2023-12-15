using AutoMapper;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.DAL.Filters;
using GameHub.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : BaseController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService,
            IMapper mapper,
            ILogger<BaseController> logger) : base(mapper, logger)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public Task<IActionResult> GetAsync([FromQuery] GameFilter filter)
        {
            return ProcessRequestAsync<List<GameModel>, List<GameViewModel>>(() =>
                _gameService.GetAllByFilterAsync(filter));
        }
    }
}
