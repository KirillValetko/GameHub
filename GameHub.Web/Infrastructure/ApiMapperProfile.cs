using AutoMapper;
using GameHub.BLL.Models;
using GameHub.Common.Models;
using GameHub.Web.Models.DtoModels;
using GameHub.Web.Models.ViewModels;

namespace GameHub.Web.Infrastructure
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<LoginDto, LoginModel>();

            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, UserViewModel>();
            CreateMap<PaginationResponse<UserModel>, PaginationResponse<UserViewModel>>();

            CreateMap<GameModel, GameViewModel>();

            CreateMap<GameDifficultyDto, GameDifficultyModel>();
            CreateMap<GameDifficultyModel, GameDifficultyViewModel>();

            CreateMap<UserGameStatsDto, UserGameStatsModel>()
                .ForMember(dest => dest.BestTime,
                    opt => opt.MapFrom(src => src.Time));
            CreateMap<UserGameStatsModel, UserGameStatsViewModel>();
            CreateMap<PaginationResponse<UserGameStatsModel>, PaginationResponse<UserGameStatsViewModel>>();
        }
    }
}
