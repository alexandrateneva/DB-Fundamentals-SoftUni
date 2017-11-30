namespace BusTicketsSystem.Data.EntityConfiguration
{
    using BusTicketsSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.TripId);

            builder.Property(e => e.Status)
                .IsRequired();

            builder.Property(e => e.DepartureTime)
                .IsRequired()
                .HasColumnType("DATETIME2");

            builder.Property(e => e.ArrivalTime)
                .IsRequired()
                .HasColumnType("DATETIME2");

            builder.HasOne(e => e.OriginBusStation)
                .WithMany(bc => bc.BusStationDepartureTrips)
                .HasForeignKey(e => e.OriginBusStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.DestinationBusStation)
                .WithMany(bc => bc.BusStationArrivalTrips)
                .HasForeignKey(e => e.DestinationBusStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Company)
                .WithMany(s => s.Trips)
                .HasForeignKey(e => e.CompanyId);
        }
    }
}
