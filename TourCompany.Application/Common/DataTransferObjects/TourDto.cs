namespace TourCompany.Application.Common.DataTransferObjects
{
    public class TourDto
    {
        public int TourID { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<DestinationDto> Destinations { get; set; } = null!;
    }
}
