using TourCompany.Domain.SeedWork;

namespace TourCompany.Domain.Entities
{
    public class Invoice : BaseAuditableEntity
    { 
        public Invoice(string invoiceNo, DateTime date, int touristID, string createdBy, DateTime created, string? updatedBy = null, DateTime? updated = null) : base(createdBy, created, updatedBy, updated)
        {
            if (string.IsNullOrWhiteSpace(invoiceNo))
                throw new ArgumentException($"'{nameof(invoiceNo)}' cannot be null or whitespace.", nameof(invoiceNo));

            _invoiceNo = invoiceNo;
            _date = date;
            _touristID = touristID;
            _invoiceItems = new();
        }

        #region Properties
        public string InvoiceNo => _invoiceNo;
        public DateTime Date => _date;
        public int TouristID => _touristID;
        #endregion

        #region Fields
        private readonly int _touristID;
        private readonly DateTime _date;
        private readonly string _invoiceNo;
        private List<InvoiceItem> _invoiceItems;
        #endregion

        #region Methods
        public bool AddInvoiceItem(InvoiceItem item)
        {
            var existingItem = (from it in _invoiceItems
                                where it.BookingID == item.BookingID && it.TouristID == item.TouristID
                                select it).Any();
            if (!existingItem)
            {
                _invoiceItems.Add(item);
                // TODO: InvoiceItem Added Event
                return true;
            }
            else
            {
                // TODO: InvoiceItem Exists Event
                return false;
            }
        } 
        #endregion

        #region Navigation Properties
        public IReadOnlyCollection<InvoiceItem> InvoiceItems => _invoiceItems.AsReadOnly();
        public Tourist Tourist { get; set; } = null!; 
        #endregion
    }
}
