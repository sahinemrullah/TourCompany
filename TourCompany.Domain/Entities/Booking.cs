namespace TourCompany.Domain.Entities
{
    public class Booking
    {
        public Booking()
        {
            Tourists = new HashSet<TourParticipant>();
        }
        public int BookingID { get; set; }
        public int TourID { get; set; }
        public int GuideID { get; set; }
        public DateTime Date { get; set; }
        public Tour Tour { get; set; } = null!;
        public Guide Guide { get; set; } = null!;
        public ICollection<TourParticipant> Tourists { get; set; }
    }
}
