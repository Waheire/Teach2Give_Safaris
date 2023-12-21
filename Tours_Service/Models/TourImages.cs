using System.ComponentModel.DataAnnotations.Schema;

namespace Tours_Service.Models
{
    public class TourImage
    {
        public Guid TourImageId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        [ForeignKey("TourId")]
        public Tour tour { get; set; } = default!;
        public Guid TourId { get; set; }
    }
}
