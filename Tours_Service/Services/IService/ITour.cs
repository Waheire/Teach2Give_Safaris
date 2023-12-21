using Tours_Service.Models;
using Tours_Service.Models.Dtos;

namespace Tours_Service.Services.IService
{
    public interface ITour
    {
        Task<List<ToursAndImagesResponseDto>> GetAllToursAsync();

        Task<Tour> GetTourByIdAsync(Guid tourId);

        Task<string> AddNewTourAsync(Tour tour);

    }
}
