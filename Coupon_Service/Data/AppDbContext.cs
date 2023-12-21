using Coupon_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupon_Service.Data
{
    public class AppDbContext:DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
