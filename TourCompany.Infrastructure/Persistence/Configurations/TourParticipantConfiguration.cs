using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Configurations
{
    public class TourParticipantConfiguration : IEntityTypeConfiguration<TourParticipant>
    {
        public void Configure(EntityTypeBuilder<TourParticipant> builder)
        {
            builder.HasKey(tp => new { tp.BookingID, tp.TouristID });

            builder.HasOne(tp => tp.Tourist)
                .WithMany(t => t.Tours)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tp => tp.Booking)
                .WithMany(t => t.Tourists)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
