namespace TourCompany.Application.Common.DataTransferObjects
{
    public class InvoiceDto
    {
        public int InvoiceID { get; set; }
        public string InvoiceNo { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public int TouristID { get; set; }
        public string Tourist { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
