using System.ComponentModel.DataAnnotations;

namespace Tours_Service.Models
{
    public class Tour
    {
        [Key]
        public Guid TourId { get; set; }
        public string TourName { get; set; } = string.Empty;
        public string TourDescription { get; set; } = string.Empty;
        public List<TourImage> TourImages { get; set; } = new List<TourImage>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
    }
}
