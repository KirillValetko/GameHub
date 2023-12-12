using AutoMapper;
using GameHub.DAL.DataModels;
using GameHub.DAL.Infrastructure.MapperValueResolvers;
using GameHub.DAL.Models;

namespace GameHub.DAL.Infrastructure
{
    public class DalMapperProfile : Profile
    {
        public DalMapperProfile()
        {
            CreateMap<User, UserDataModel>()
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom<UserValueResolver>());
            CreateMap<UserDataModel, User>()
                .ForMember(dest => dest.Password,
                    opt => opt.Condition(src => 
                        !string.IsNullOrEmpty(src.Password)))
                .ForMember(dest => dest.Login, 
                    opt => opt.Condition(src =>
                        !string.IsNullOrEmpty(src.Login)))
                .ForMember(dest => dest.UserName,
                    opt => opt.Condition(src => 
                        !string.IsNullOrEmpty(src.UserName)))
                .ForMember(dest => dest.Role,
                    opt => opt.Ignore());
        }
    }
}
