namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    public class PaymentMethodsConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Type)
                .IsRequired();

            builder.HasIndex(e => new {e.UserId, e.BankAccountId, e.CreditCardId}).IsUnique();

            builder.HasOne(c => c.User)
                .WithMany(r => r.PaymentMethods)
                .HasForeignKey(c => c.UserId);

            builder.HasOne(c => c.BankAccount)
                .WithOne(r => r.PaymentMethod)
                .HasForeignKey<PaymentMethod>(c => c.BankAccountId);

            builder.HasOne(c => c.CreditCard)
                .WithOne(r => r.PaymentMethod)
                .HasForeignKey<PaymentMethod>(c => c.CreditCardId);
        }
    }
}
