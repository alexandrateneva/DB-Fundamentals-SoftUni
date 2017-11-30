namespace BusTicketsSystem.Data.EntityConfiguration
{
    using BusTicketsSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StationConfig : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(e => e.StationId);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);
            
            builder.HasOne(e => e.Town)
                .WithMany(t => t.Stations)
                .HasForeignKey(e => e.TownId);
        }
    }
}
