namespace TourCompany.Domain.Entities
{
    public class InvoiceItem
    {
        public int InvoiceID { get; set; }
        public int BookingID { get; set; }
        public int TouristID { get; set; }
        public decimal Price { get; set; }
        public Invoice Invoice { get; set; } = null!;
        public TourParticipant TourParticipant { get; set; } = null!;
    }
}
