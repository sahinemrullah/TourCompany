using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(g => g.Name)
                .HasColumnType("nchar")
                .HasMaxLength(5);
        }
    }
}
