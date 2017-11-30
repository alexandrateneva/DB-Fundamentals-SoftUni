namespace BusTicketsSystem.Data
{
    using BusTicketsSystem.Data.EntityConfiguration;
    using BusTicketsSystem.Models;
    using Microsoft.EntityFrameworkCore;

    public class BusTicketSystemContext : DbContext
    {
        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Trip> Trips { get; set; }
        
        public BusTicketSystemContext()
        {
        }

        public BusTicketSystemContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankAccountConfig());

            modelBuilder.ApplyConfiguration(new CompanyConfig());

            modelBuilder.ApplyConfiguration(new CustomerConfig());

            modelBuilder.ApplyConfiguration(new ReviewConfig());

            modelBuilder.ApplyConfiguration(new StationConfig());

            modelBuilder.ApplyConfiguration(new TicketConfig());

            modelBuilder.ApplyConfiguration(new TownConfig());

            modelBuilder.ApplyConfiguration(new TripConfig());
        }
    }
}
