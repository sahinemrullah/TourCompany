namespace TourCompany.Application.Common.DataTransferObjects
{
    public class TourVm
    {
        public string Name { get; set; } = null!;
        public IEnumerable<DestinationDto> Destinations { get; set; } = null!;
        public IEnumerable<BookingDto> Bookings { get; set; } = null!;
    }
}
