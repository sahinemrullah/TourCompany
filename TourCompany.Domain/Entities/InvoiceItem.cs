namespace TourCompany.Domain.Entities
{
    public class InvoiceItem
    {
        public InvoiceItem(double appliedDiscount, decimal price, string title, int touristID, int bookingID, int invoiceID)
        {
            _appliedDiscount = appliedDiscount;
            _price = price;
            _title = title;
            _touristID = touristID;
            _bookingID = bookingID;
            _invoiceID = invoiceID;
        }

        #region Properties
        public int InvoiceID => _invoiceID;
        public int BookingID => _bookingID;
        public int TouristID => _touristID;
        public string Title => _title;
        public decimal Price => _price;
        public double AppliedDiscount => _appliedDiscount;
        #endregion

        #region Fields
        private readonly double _appliedDiscount;
        private readonly decimal _price;
        private readonly string _title;
        private readonly int _touristID;
        private readonly int _bookingID;
        private readonly int _invoiceID;
        #endregion
    }
}
