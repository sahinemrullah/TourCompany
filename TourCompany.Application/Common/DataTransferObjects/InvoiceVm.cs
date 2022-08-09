namespace TourCompany.Application.Common.DataTransferObjects
{
    public class InvoiceVm
    {
        public int InvoiceID { get; internal set; }
        public int TouristID { get; internal set; }
        public string InvoiceNo { get; internal set; } = null!;
        public string Tourist { get; internal set; } = null!;
        public DateTime Date { get; internal set; }
        public IEnumerable<InvoiceItemDto> Items { get; internal set; } = null!;
    }
}
