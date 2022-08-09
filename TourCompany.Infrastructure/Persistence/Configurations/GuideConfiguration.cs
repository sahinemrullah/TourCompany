using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Configurations
{
    public class GuideConfiguration : IEntityTypeConfiguration<Guide>
    {
        public void Configure(EntityTypeBuilder<Guide> builder)
        {
            builder.Property(g => g.Name)
                .HasMaxLength(20);

            builder.Property(g => g.Surname)
                .HasMaxLength(40);

            builder.Property(g => g.TelephoneNumber)
                .HasColumnType("char")
                .HasMaxLength(10);
            builder.Property(g => g.IsActive).HasDefaultValue(true);
        }
    }
}
