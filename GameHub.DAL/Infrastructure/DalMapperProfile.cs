using AutoMapper;
using GameHub.DAL.DataModels;
using GameHub.DAL.Models;

namespace GameHub.DAL.Infrastructure
{
    public class DalMapperProfile : Profile
    {
        public DalMapperProfile()
        {
            CreateMap<User, UserDataModel>();
            CreateMap<UserDataModel, User>()
                .ForMember(dest => dest.Password,
                    opt => opt.Condition(src => 
                        !string.IsNullOrEmpty(src.Password)))
                .ForMember(dest => dest.Login, 
                    opt => opt.Condition(src =>
                        !string.IsNullOrEmpty(src.Login)))
                .ForMember(dest => dest.UserName,
                    opt => opt.Condition(src => !string.IsNullOrEmpty(src.UserName)));
        }
    }
}
