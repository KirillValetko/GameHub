using AutoMapper;
using GameHub.BLL.Models;
using GameHub.Web.Models.DtoModels;
using GameHub.Web.Models.ViewModels;

namespace GameHub.Web.Infrastructure
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, UserViewModel>();
        }
    }
}
