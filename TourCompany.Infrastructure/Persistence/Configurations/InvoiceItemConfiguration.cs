using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Configurations
{
    public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(e => new { e.InvoiceID, e.BookingID, e.TouristID });

            builder.Property(e => e.Price)
                .HasPrecision(9, 2);

            builder.HasOne(e => e.Invoice)
                .WithMany(e => e.InvoiceItems)
                .HasPrincipalKey(e => new { e.InvoiceID, e.TouristID });

            builder.HasOne(e => e.TourParticipant)
                .WithOne()
                .HasForeignKey<InvoiceItem>(it => new { it.BookingID, it.TouristID });
        }
    }
}
