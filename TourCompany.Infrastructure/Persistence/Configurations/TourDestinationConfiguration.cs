using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Configurations
{
    public class TourDestinationConfiguration : IEntityTypeConfiguration<TourDestination>
    {
        public void Configure(EntityTypeBuilder<TourDestination> builder)
        {
            builder.HasKey(tsd => new { tsd.TourID, tsd.DestinationID });
        }
    }
}
