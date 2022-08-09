namespace TourCompany.Domain.Entities
{
    public class Tour
    {
        public Tour()
        {
            Bookings = new HashSet<Booking>();
            Destinations = new HashSet<TourDestination>();
        }
        public int TourID { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; }
        public ICollection<TourDestination> Destinations { get; set; }
    }
}
