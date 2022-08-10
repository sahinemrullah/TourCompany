using TourCompany.Domain.SeedWork;

namespace TourCompany.Domain.Entities
{
    public class Destination : BaseAuditableEntity
    {
        public Destination(string name, decimal price, string createdBy, DateTime created, string? updatedBy = null, DateTime? updated = null) : base(createdBy, created, updatedBy, updated)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

            _name = name;
            _price = price;
        }

        public string Name { get => _name; set => _name = value; }
        public decimal Price { get => _price; set => _price = value; }

        private string _name;
        private decimal _price;
    }
}
