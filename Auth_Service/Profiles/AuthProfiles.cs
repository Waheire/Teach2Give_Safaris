using Auth_Service.Models;
using Auth_Service.Models.Dtos;
using AutoMapper;

namespace Auth_Service.Profiles
{
    public class AuthProfiles:Profile
    {
        public AuthProfiles()
        {
            CreateMap<RegisterUserDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, src => src.MapFrom(r => r.Email));

            CreateMap<UserResponseDto, ApplicationUser>().ReverseMap();
        }
    }
}
