namespace Tours_Service.Models.Dtos
{
    public class ToursAndImagesResponseDto
    {
        public Guid Id { get; set; }
        public string TourName { get; set; } = string.Empty;
        public string TourDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }

        public List<AddTourImageDto> TourImages { get; set; } = new List<AddTourImageDto>();
    }
}
