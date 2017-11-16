namespace P03_FootballBetting.Data
{
    using Microsoft.EntityFrameworkCore;
    using P03_FootballBetting.Data.Models;

    public class FootballBettingContext : DbContext
    {
        public DbSet<Bet> Bets { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public FootballBettingContext()
        {
        }

        public FootballBettingContext(DbContextOptions options)
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
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(p => p.TeamId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(p => p.LogoUrl)
                    .IsUnicode(false);

                entity.Property(p => p.Initials)
                    .IsRequired()
                    .HasColumnType("NCHAR(3)");

                entity.Property(p => p.LogoUrl)
                    .IsUnicode(false);

                entity.Property(p => p.Initials)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasColumnType("CHAR(3)");

                entity.HasOne(e => e.PrimaryKitColor)
                    .WithMany(c => c.PrimaryKitTeams)
                    .HasForeignKey(e => e.PrimaryKitColorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SecondaryKitColor)
                    .WithMany(c => c.SecondaryKitTeams)
                    .HasForeignKey(e => e.SecondaryKitColorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Town)
                    .WithMany(c => c.Teams)
                    .HasForeignKey(e => e.TownId);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(p => p.ColorId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasKey(p => p.TownId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(e => e.Country)
                    .WithMany(c => c.Towns)
                    .HasForeignKey(e => e.CountryId);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(p => p.CountryId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.PlayerId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.IsInjured)
                    .HasDefaultValue(false);

                entity.HasOne(e => e.Team)
                    .WithMany(c => c.Players)
                    .HasForeignKey(e => e.PlayerId);

                entity.HasOne(e => e.Position)
                    .WithMany(c => c.Players)
                    .HasForeignKey(e => e.PositionId);
            });

            modelBuilder.Entity<Position>(entity =>
             {
                 entity.HasKey(p => p.PositionId);

                 entity.Property(p => p.Name)
                     .IsRequired()
                     .HasMaxLength(30);
             });

            modelBuilder.Entity<PlayerStatistic>(entity =>
            {
                entity.HasKey(p => new { p.PlayerId, p.GameId });

                entity.HasOne(e => e.Player)
                    .WithMany(c => c.PlayerStatistics)
                    .HasForeignKey(e => e.PlayerId);

                entity.HasOne(e => e.Game)
                    .WithMany(c => c.PlayerStatistics)
                    .HasForeignKey(e => e.GameId);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(p => p.GameId);

                entity.HasOne(e => e.HomeTeam)
                    .WithMany(c => c.HomeGames)
                    .HasForeignKey(e => e.HomeTeamId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(e => e.AwayTeam)
                    .WithMany(c => c.AwayGames)
                    .HasForeignKey(e => e.AwayTeamId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(p => p.BetId);

                entity.HasOne(e => e.Game)
                    .WithMany(c => c.Bets)
                    .HasForeignKey(e => e.GameId);

                entity.HasOne(e => e.User)
                    .WithMany(c => c.Bets)
                    .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(p => p.UserId);

                entity.Property(p => p.Name)
                    .HasMaxLength(100);

                entity.Property(p => p.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(p => p.Email)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(20);

                entity.Property(p => p.Balance)
                    .HasDefaultValue(0.0m);
            });
        }
    }
}
