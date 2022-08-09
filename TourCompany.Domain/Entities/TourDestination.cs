namespace TourCompany.Domain.Entities
{
    public class TourDestination
    {
        public int TourID { get; set; }
        public int DestinationID { get; set; }
        public Tour Tour { get; set; } = null!;
        public Destination Destination { get; set; } = null!;
    }
}
