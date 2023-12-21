using AutoMapper;
using Tours_Service.Models;
using Tours_Service.Models.Dtos;

namespace Tours_Service.Profiles
{
    public class ToursProfiles:Profile
    {
        public ToursProfiles()
        {
            CreateMap<AddTourDto, Tour>().ReverseMap();
            CreateMap<AddTourImageDto, TourImage>().ReverseMap();
        }
    }
}
