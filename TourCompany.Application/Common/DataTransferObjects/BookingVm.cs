namespace TourCompany.Application.Common.DataTransferObjects
{
    public class BookingVm
    {
        public int BookingID { get; set; }
        public DateTime Date { get; set; }
        public GuideDto Guide { get; set; } = null!;
        public TourDto Tour { get; set; } = null!;
        public IEnumerable<TouristDto> Tourists { get; set; } = null!;
    }
}
