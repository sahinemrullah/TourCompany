using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Configurations
{
    public class TouristConfiguration : IEntityTypeConfiguration<Tourist>
    {
        public void Configure(EntityTypeBuilder<Tourist> builder)
        {

            builder.Property(t => t.Name)
                .HasMaxLength(20);

            builder.Property(t => t.Surname)
                .HasMaxLength(40);

            builder.Property(t => t.BirthDate)
                .HasColumnType("date");
        }
    }
}
