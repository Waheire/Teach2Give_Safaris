namespace Tours_Service.Models.Dtos
{
    public class AddTourDto
    {
        public string TourName { get; set; } = string.Empty;
        public string TourDescription { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
    }
}
