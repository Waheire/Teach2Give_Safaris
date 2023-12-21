using Microsoft.EntityFrameworkCore;
using Tours_Service.Data;
using Tours_Service.Models;
using Tours_Service.Services.IService;

namespace Tours_Service.Services
{
    public class TourImageService : ITourImage
    {
        private readonly AppDbContext _context;

        public TourImageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddTourImageAsync(Guid Id, TourImage images)
        {
            //find the tour
            var tour = await _context.Tours.Where(t => t.TourId == Id).FirstOrDefaultAsync();
            tour.TourImages.Add(images);
            await _context.SaveChangesAsync();
            return "Image Added!";
        }
    }
}
