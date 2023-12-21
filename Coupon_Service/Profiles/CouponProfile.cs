using AutoMapper;
using Coupon_Service.Models;
using Coupon_Service.Models.Dtos;

namespace Coupon_Service.Profiles
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<AddCouponDto, Coupon>().ReverseMap();
        }
    }
}
