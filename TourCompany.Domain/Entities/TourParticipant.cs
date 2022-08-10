namespace TourCompany.Domain.Entities
{
    public class TourParticipant
    {
        private readonly int _touristID;
        private readonly int _bookingID;

        public TourParticipant(int touristID, int bookingID)
        {
            _touristID = touristID;
            _bookingID = bookingID;
        }

        public int BookingID => _bookingID;
        public int TouristID => _touristID;
        public Tourist Tourist { get; set; } = null!;
    }
}
