namespace BusTicketsSystem.Data.EntityConfiguration
{
    using BusTicketsSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(e => e.TicketId);

            builder.Property(e => e.Price)
                .IsRequired();

            builder.Property(e => e.Seat)
                .IsRequired()
                .HasMaxLength(10);
            
            builder.HasOne(e => e.Customer)
                .WithMany(s => s.Tickets)
                .HasForeignKey(e => e.CustomerId);

            builder.HasOne(e => e.Trip)
                .WithMany(s => s.Tickets)
                .HasForeignKey(e => e.TripId);
        }
    }
}
