using AutoMapper;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.Common.Constants;
using GameHub.DAL.Filters;
using GameHub.Web.Infrastructure.Attributes;
using GameHub.Web.Models.DtoModels;
using GameHub.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameHub.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameDifficultyController : BaseController
    {
        private readonly IGameDifficultyService _gameDifficultyService;

        public GameDifficultyController(IGameDifficultyService gameDifficultyService,
            IMapper mapper,
            ILogger<BaseController> logger) : base(mapper, logger)
        {
            _gameDifficultyService = gameDifficultyService;
        }

        [HttpGet]
        [AllowAnonymous]
        public Task<IActionResult> GetAsync([FromQuery] GameDifficultyFilter filter)
        {
            return ProcessRequestAsync<List<GameDifficultyModel>, List<GameDifficultyViewModel>>(() =>
                _gameDifficultyService.GetAllByFilterAsync(filter));
        }

        [HttpPost]
        [RequiresRoleClaim(RoleConstants.Admin)]
        public Task<IActionResult> PostAsync(GameDifficultyDto item)
        {
            var mappedItem = _mapper.Map<GameDifficultyModel>(item);

            return ProcessRequestAsync<GameDifficultyViewModel>(() =>
                _gameDifficultyService.CreateAsync(mappedItem));
        }

        [HttpPut]
        [RequiresRoleClaim(RoleConstants.Admin)]
        public Task<IActionResult> PutAsync(GameDifficultyDto item)
        {
            var mappedItem = _mapper.Map<GameDifficultyModel>(item);

            return ProcessRequestAsync<GameDifficultyViewModel>(() =>
                _gameDifficultyService.UpdateAsync(mappedItem));
        }

        //[HttpDelete]
        //[RequiresRoleClaim(RoleConstants.Admin)]
        //public Task<IActionResult> DeleteAsync(string id)
        //{
        //    return ProcessRequestAsync<GameDifficultyViewModel>(() =>
        //        _gameDifficultyService.DeleteAsync(id));
        //}
    }
}
