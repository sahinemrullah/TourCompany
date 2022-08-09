namespace TourCompany.Domain.Entities
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }
        public int InvoiceID { get; set; }
        public string InvoiceNo { get; set; } = null!;
        public DateTime Date { get; set; }
        public int TouristID { get; set; }
        public double Discount { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
        public Tourist Tourist { get; set; } = null!;
    }
}
