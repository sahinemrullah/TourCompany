namespace TourCompany.Domain.Entities
{
    public class TourParticipant
    {
        public int BookingID { get; set; }
        public int TouristID { get; set; }
        public Booking Booking { get; set; } = null!;
        public Tourist Tourist { get; set; } = null!;
    }
}
