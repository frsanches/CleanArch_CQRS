using Banking.Application.Interfaces;
using Banking.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("Database"));
            });

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            return services;
        }
    }
}