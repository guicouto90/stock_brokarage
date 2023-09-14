using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockBrokarageChallenge.Application.Shared.Data.Context;
using StockBrokarageChallenge.Application.Shared.Data.Repository;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;

namespace StockBrokarageChallenge.Application.Shared.Data
{
    public static class DataExtension
    {
        public static IServiceCollection AddDbInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockHistoryPriceRepository, StockHistoryPriceRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();

            return services;
        }
    }
}
