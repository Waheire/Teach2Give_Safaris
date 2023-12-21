using Coupon_Service.Data;
using Coupon_Service.Models;
using Coupon_Service.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Coupon_Service.Services
{
    public class CouponService : ICoupon
    {
        private readonly AppDbContext _context;

        public CouponService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddCouponAsync(Coupon coupon)
        {
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            return "Coupon Added Successfully!";
        }

        public async Task<string> DeleteCouponAsync(Coupon coupon)
        {
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return "Coupon Removed Successfully!";
        }

        public async Task<List<Coupon>> GetAllCouponsAsync()
        {
            return await _context.Coupons.ToListAsync();
        }

        public async Task<Coupon> GetCouponByIdAsync(Guid couponId)
        {
            return await _context.Coupons.Where(c => c.CouponId == couponId).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateCouponAsync()
        {
            await _context.SaveChangesAsync();
            return "Coupon Updated Successfully!";
        }
    }
}
