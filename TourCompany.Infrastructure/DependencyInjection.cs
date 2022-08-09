using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TourCompany.Application.Common.Interfaces;
using TourCompany.Infrastructure.Persistence;
using TourCompany.Infrastructure.Persistence.Interceptors;

namespace TourCompany.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TourCompanyDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("TourCompanyDB"),
                        b => b.MigrationsAssembly(typeof(TourCompanyDbContext).Assembly.FullName))
                .AddInterceptors(new InvoiceInsertInterceptor()));

            services.AddScoped<ITourCompanyDbContext>(provider => provider.GetRequiredService<TourCompanyDbContext>());

            return services;
        }
    }
}
