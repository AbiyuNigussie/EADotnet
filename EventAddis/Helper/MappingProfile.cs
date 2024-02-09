using AutoMapper;
using EventAddis.Dto;
using EventAddis.Models;

namespace EventAddis.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserInfo, UserDetailsDto>().ReverseMap();
            CreateMap<UserCredential, UserDetailsDto>().ReverseMap();
        }
    }
}
