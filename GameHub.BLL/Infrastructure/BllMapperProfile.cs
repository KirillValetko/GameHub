using AutoMapper;
using GameHub.BLL.Models;
using GameHub.Common.Models;
using GameHub.DAL.DataModels;

namespace GameHub.BLL.Infrastructure
{
    public class BllMapperProfile : Profile
    {
        public BllMapperProfile()
        {
            CreateMap<UserDataModel, UserModel>();
            CreateMap<UserModel, UserDataModel>()
                .ForMember(dest => dest.Password,
                    opt => opt.Condition(src => 
                        !string.IsNullOrEmpty(src.Password)))
                .ForMember(dest => dest.Login,
                    opt => opt.Condition(src =>
                        !string.IsNullOrEmpty(src.Login)));
            CreateMap<PaginationResponse<UserDataModel>, PaginationResponse<UserModel>>();

            CreateMap<GameDataModel, GameModel>();
            CreateMap<GameModel, GameDataModel>();

            CreateMap<GameDifficultyDataModel, GameDifficultyModel>();
            CreateMap<GameDifficultyModel, GameDifficultyDataModel>();

            CreateMap<UserGameStatsDataModel, UserGameStatsModel>();
            CreateMap<UserGameStatsModel, UserGameStatsDataModel>();
            CreateMap<PaginationResponse<UserGameStatsDataModel>, PaginationResponse<UserGameStatsModel>>();
        }
    }
}
