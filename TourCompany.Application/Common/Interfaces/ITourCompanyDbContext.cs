using Microsoft.EntityFrameworkCore;
using TourCompany.Domain.Entities;

namespace TourCompany.Application.Common.Interfaces
{
    public interface ITourCompanyDbContext : IDisposable
    {
        public DbContext Instance { get; }
        public DbSet<Country> Countries { get; }
        public DbSet<Destination> Destinations { get; }
        public DbSet<Guide> Guides { get; }
        public DbSet<GuideLanguage> GuideLanguages { get; }
        public DbSet<Language> Languages { get; }
        public DbSet<Nationality> Nationalities { get; }
        public DbSet<Tour> Tours { get; }
        public DbSet<Tourist> Tourists { get; }
        public DbSet<Booking> Bookings { get; }
        public DbSet<TourDestination> TourDestinations { get; }
        public DbSet<TourParticipant> TourParticipants { get; }
        public DbSet<Gender> Genders { get; }
        public DbSet<Invoice> Invoices { get; }
        public DbSet<InvoiceItem> InvoiceItems { get; }
    }
}
