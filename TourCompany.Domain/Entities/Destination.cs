namespace TourCompany.Domain.Entities
{
    public class Destination
    {
        public int DestinationID { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
