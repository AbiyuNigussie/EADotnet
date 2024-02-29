using AutoMapper;
using EventAddis.Entity;
using EventAddis.Models;
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
            CreateMap<UserInfo, UserProfile>();
            CreateMap<RegisterEvent, Event>();
            CreateMap<RegisterEvent, Location>();
            CreateMap<RegisterCity, City>();
            CreateMap<CreateCategory, Category>();
        }
        
    }
}
