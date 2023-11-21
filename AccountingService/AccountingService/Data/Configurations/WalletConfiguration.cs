using AccountingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountingService.Data.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable(nameof(Wallet));

            builder
                .Property(x => x.Id)
                .IsRequired();
            builder
                .Property(x => x.Amount)
                .HasColumnType("money")
                .IsRequired();
            builder
                .Property(x => x.ClientId)
                .IsRequired();

            builder.HasKey(x => x.Id);
        }
    }
}