using Microsoft.EntityFrameworkCore;
using StockBrokarageChallenge.Application.Shared.Models;

namespace StockBrokarageChallenge.Application.Shared.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockHistoryPrice> StocksHistoryPrices { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<TransactionHistory> TransactionsHistory { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<StocksWallet> StocksWallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
