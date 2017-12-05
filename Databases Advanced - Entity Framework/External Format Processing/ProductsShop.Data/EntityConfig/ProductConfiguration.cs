namespace ProductsShop.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProductsShop.Models;

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(200);
            
            builder.HasOne(e => e.Buyer)
                .WithMany(s => s.ProductsBought)
                .HasForeignKey(e => e.BuyerId);

            builder.HasOne(e => e.Seller)
                .WithMany(s => s.ProductsSold)
                .HasForeignKey(e => e.SellerId);
        }
    }
}
