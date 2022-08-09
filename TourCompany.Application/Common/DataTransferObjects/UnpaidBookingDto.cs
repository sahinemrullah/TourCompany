namespace TourCompany.Application.Common.DataTransferObjects
{
    public class UnpaidBookingDto
    {
        public int BookingID { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
