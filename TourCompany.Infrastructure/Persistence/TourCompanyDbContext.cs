using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Domain.Entities;

namespace TourCompany.Infrastructure.Persistence
{
    public class TourCompanyDbContext : DbContext, ITourCompanyDbContext
    {
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<Destination> Destinations => Set<Destination>();
        public DbSet<Guide> Guides => Set<Guide>();
        public DbSet<GuideLanguage> GuideLanguages => Set<GuideLanguage>();
        public DbSet<Language> Languages => Set<Language>();
        public DbSet<Nationality> Nationalities => Set<Nationality>();
        public DbSet<Tour> Tours => Set<Tour>();
        public DbSet<Tourist> Tourists => Set<Tourist>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<TourDestination> TourDestinations => Set<TourDestination>();
        public DbSet<TourParticipant> TourParticipants => Set<TourParticipant>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
        public DbSet<Gender> Genders => Set<Gender>();
        public DbContext Instance => this;
        public TourCompanyDbContext(DbContextOptions<TourCompanyDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
