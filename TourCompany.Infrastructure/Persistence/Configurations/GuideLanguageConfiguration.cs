using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence.Configurations
{
    public class GuideLanguageConfiguration : IEntityTypeConfiguration<GuideLanguage>
    {
        public void Configure(EntityTypeBuilder<GuideLanguage> builder)
        {
            builder.HasKey(gl => new { gl.GuideID, gl.LanguageID });
        }
    }
}
