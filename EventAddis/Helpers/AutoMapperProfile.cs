using AutoMapper;
using EventAddis.Entity;
using WebService.API.Entity;
using WebService.API.Models;

namespace WebService.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserInfo, UserModel>();
            CreateMap<RegisterUser, UserInfo>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<RegisterUser, UserCredential>();
            CreateMap<UpdateUser, UserInfo>();
        }
        
    }
}
