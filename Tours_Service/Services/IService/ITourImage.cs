using Tours_Service.Models;

namespace Tours_Service.Services.IService
{
    public interface ITourImage
    {
        Task<string> AddTourImageAsync(Guid Id, TourImage images);
    }
}
