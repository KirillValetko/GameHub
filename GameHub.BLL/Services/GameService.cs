using AutoMapper;
using GameHub.BLL.Models;
using GameHub.BLL.Services.Interfaces;
using GameHub.DAL.DataModels;
using GameHub.DAL.Filters;
using GameHub.DAL.Models;
using GameHub.DAL.Repositories.Interfaces;

namespace GameHub.BLL.Services
{
    public class GameService :
        BaseService<Game, GameDataModel, GameModel, GameFilter>,
        IGameService
    {
        public GameService(IGameRepository repository,
            IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
