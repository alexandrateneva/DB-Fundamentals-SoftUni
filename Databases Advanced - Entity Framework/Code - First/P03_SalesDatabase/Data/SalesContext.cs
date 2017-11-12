namespace P03_SalesDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_SalesDatabase.Data.Models;

    public class SalesContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Store> Stores { get; set; }


        public SalesContext()
        {
        }

        public SalesContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
                builder.UseSqlServer(Configuration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(p => p.CustomerId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(100);

                entity.Property(p => p.Email)
                    .IsUnicode(false)
                    .HasMaxLength(50);

                entity.Property(p => p.CreditCardNumber)
                     .IsRequired()
                     .IsUnicode(true);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(p => p.Quantity)
                    .IsRequired();

                entity.Property(p => p.Price)
                    .IsRequired()
                    .HasColumnType("DECIMAL");

                entity.Property(p => p.Description)
                    .IsUnicode(true)
                    .HasMaxLength(250)
                    .HasDefaultValue("No description");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(p => p.StoreId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(p => p.SaleId);

                entity.Property(p => p.Date)
                    .HasDefaultValueSql("GETDATE()")
                    .IsRequired();

                entity.HasOne(p => p.Product)
                    .WithMany(s => s.Sales)
                    .HasForeignKey(p => p.ProductId);

                entity.HasOne(c => c.Customer)
                    .WithMany(s => s.Sales)
                    .HasForeignKey(c => c.CustomerId);

                entity.HasOne(s => s.Store)
                    .WithMany(ss => ss.Sales)
                    .HasForeignKey(s => s.StoreId);
            });
        }
    }
}
