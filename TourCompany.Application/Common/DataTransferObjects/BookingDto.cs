namespace TourCompany.Application.Common.DataTransferObjects
{
    public class BookingDto
    {
        public int BookingID { get; set; }
        public DateTime Date { get; set; }
        public int GuideID { get; set; }
        public string Guide { get; set; } = null!;
        public int TourID { get; set; }
        public string Tour { get; set; } = null!;
    }
}