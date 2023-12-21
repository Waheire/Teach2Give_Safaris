using Coupon_Service.Models;

namespace Coupon_Service.Services.IServices
{
    public interface ICoupon
    {
        Task<List<Coupon>> GetAllCouponsAsync();

        Task<Coupon> GetCouponByIdAsync(Guid couponId);

        Task<string> AddCouponAsync(Coupon coupon);

        Task<string> UpdateCouponAsync();

        Task<string> DeleteCouponAsync(Coupon coupon);
    }
}
