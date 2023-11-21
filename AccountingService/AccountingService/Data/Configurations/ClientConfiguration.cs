using AccountingService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountingService.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable(nameof(Client));

            builder
                .Property(x => x.Id)
                .IsRequired();
            builder
                .Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(x => x.MiddleName)
                .HasMaxLength(100)
                .IsRequired();
            builder
                .Property(x => x.BirthDate)
                .IsRequired();

            builder
                .HasOne(x => x.Wallet)
                .WithOne()
                .HasForeignKey<Wallet>(x => x.ClientId)
                .IsRequired();

            builder.HasKey(x => x.Id);
        }
    }
}