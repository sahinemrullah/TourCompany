using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Configurations
{
    public class NationalityConfiguration : IEntityTypeConfiguration<Nationality>
    {
        public void Configure(EntityTypeBuilder<Nationality> builder)
        {
            builder.Property(n => n.Name)
                .HasColumnType("varchar")
                .HasMaxLength(21);
        }
    }
}
