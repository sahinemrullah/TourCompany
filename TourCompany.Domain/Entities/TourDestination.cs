namespace TourCompany.Domain.Entities
{
    public class TourDestination
    {
        public int TourID { get => _tourID; private set => _tourID = value; }
        public int DestinationID { get => _destinationID; private set => _destinationID = value; }

        private int _destinationID;
        private int _tourID;

        public Destination Destination { get; set; } = null!;
    }
}
