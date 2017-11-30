namespace BusTicketsSystem.Data.EntityConfiguration
{
    using BusTicketsSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(e => e.ReviewId);

            builder.Property(e => e.Content)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(1000);
            
            builder.Property(e => e.DateTimeOfPublishing)
                .IsRequired()
                .HasColumnType("DATETIME2");

            builder.HasOne(e => e.Company)
                .WithMany(s => s.Reviews)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Customer)
                .WithMany(s => s.Reviews)
                .HasForeignKey(e => e.CustomerId);
        }
    }
}
