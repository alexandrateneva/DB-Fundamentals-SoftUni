namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Data.Models;

    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(b => b.CreditCardId);

            builder.Ignore(b => b.LimitLeft);

            builder.Ignore(b => b.PaymentMethodId);

            builder.Property(ed => ed.ExpirationDate)
                .IsRequired()
                .HasColumnType("DATETIME2");
        }
    }
}
