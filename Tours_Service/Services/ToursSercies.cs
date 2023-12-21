using Microsoft.EntityFrameworkCore;
using Tours_Service.Data;
using Tours_Service.Models;
using Tours_Service.Models.Dtos;
using Tours_Service.Services.IService;

namespace Tours_Service.Services
{
    public class ToursSercies : ITour
    {

        private readonly AppDbContext _context;

        public ToursSercies(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddNewTourAsync(Tour tour)
        {
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();
            return "Tour Created Successfully!";
        }

        public async Task<List<ToursAndImagesResponseDto>> GetAllToursAsync()
        {
            return await _context.Tours.Select(t => new ToursAndImagesResponseDto() 
            {
                Id = t.TourId,
                TourName = t.TourName,
                TourDescription = t.TourDescription,
                Price = t.Price,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                TourImages = t.TourImages.Select(x => new AddTourImageDto() 
                {
                    ImageUrl = x.ImageUrl,
                }).ToList()
            }).ToListAsync();
        }

        public async Task<Tour> GetTourByIdAsync(Guid tourId)
        {
            return await _context.Tours.Where(t => t.TourId == tourId).FirstOrDefaultAsync();
        }
    }
}
