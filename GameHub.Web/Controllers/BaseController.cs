using AutoMapper;
using GameHub.Web.Responses;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace GameHub.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseController> _logger;

        protected BaseController(IMapper mapper,
            ILogger<BaseController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        protected async Task<IActionResult> ProcessRequestAsync<TModel, TViewModel>(Func<Task<TModel>> func)
        {
            try
            {
                var result = await func();
                var mappedResult = _mapper.Map<TViewModel>(result);

                return Ok(new ApiResponse<TViewModel>(mappedResult));
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
        }

        protected async Task<IActionResult> ProcessRequestAsync<TViewModel>(Func<Task> func)
        {
            try
            {
                await func();

                return Ok(new ApiResponse<TViewModel>());
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
        }

        protected async Task<IActionResult> ProcessRequestAsync<TViewModel>(Func<Task<TViewModel>> func)
        {
            try
            {
                var result = await func();

                return Ok(new ApiResponse<TViewModel>(result));
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
        }
    }
}
