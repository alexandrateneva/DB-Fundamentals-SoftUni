namespace ProductsShop.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using ProductsShop.Models;

    public class CategoryProductsConfiguration : IEntityTypeConfiguration<CategoryProducts>
    {
        public void Configure(EntityTypeBuilder<CategoryProducts> builder)
        {
            builder.HasKey(p => new { p.CategoryId, p.ProductId });

            builder.HasOne(e => e.Product)
                .WithMany(s => s.CategoryProducts)
                .HasForeignKey(e => e.ProductId);

            builder.HasOne(e => e.Category)
                .WithMany(s => s.CategoryProducts)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}
