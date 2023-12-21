using Microsoft.EntityFrameworkCore;
using Tours_Service.Models;

namespace Tours_Service.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourImage> TourImages { get; set; }
    }
}
