namespace BusTicketsSystem.Data.EntityConfiguration
{
    using BusTicketsSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(e => e.BankAccountId);

            builder.Property(e => e.Balance)
                .IsRequired()
                .HasDefaultValue(0m);

            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(a => a.Customer)
                .WithOne(c => c.BankAccount)
                .HasForeignKey<BankAccount>(a => a.CustomerId);
        }
    }
}
