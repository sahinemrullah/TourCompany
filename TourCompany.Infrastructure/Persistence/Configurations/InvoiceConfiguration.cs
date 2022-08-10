using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(e => e.ID)
                .UseIdentityColumn();

            builder.Property(e => e.InvoiceNo)
                .HasColumnType("char")
                .HasMaxLength(14);

            builder.Property(e => e.Date)
                .HasColumnType("date");
        }
    }
}
