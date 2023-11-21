using AccountingService.Data.Configurations;
using AccountingService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingService.Data
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
        }
    }
}