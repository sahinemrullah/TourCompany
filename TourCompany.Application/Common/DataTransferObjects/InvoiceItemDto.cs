namespace TourCompany.Application.Common.DataTransferObjects
{
    public class InvoiceItemDto
    {
        public decimal Price { get; set; }
        public string Tour { get; set; } = null!;
    }
}
