namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;

    public class HospitalContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientMedicaments { get; set; }

        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
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
            modelBuilder.Entity<Patient>(entity =>
                 {
                     entity.HasKey(p => p.PatientId);

                     entity.Property(p => p.FirstName)
                         .IsUnicode(true)
                         .HasMaxLength(50);

                     entity.Property(p => p.LastName)
                         .IsUnicode(true)
                         .HasMaxLength(50);

                     entity.Property(p => p.Address)
                         .IsUnicode(true)
                         .HasMaxLength(250);

                     entity.Property(p => p.Email)
                         .IsUnicode(false)
                         .HasMaxLength(80);

                     entity.Property(p => p.HasInsurance)
                         .HasDefaultValue(true);
                 });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.DoctorId);

                entity.Property(d => d.Name)
                    .IsUnicode(true)
                    .HasMaxLength(100);

                entity.Property(d => d.Specialty)
                    .IsUnicode(true)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Visitation>(entity =>
                 {
                     entity.HasKey(v => v.VisitationId);

                     entity.Property(v => v.Date)
                         .HasColumnType("DATETIME2")
                         .HasDefaultValueSql("GETDATE()");

                     entity.Property(v => v.Comments)
                         .IsUnicode(true)
                         .HasMaxLength(250);

                     entity.HasOne(v => v.Patient)
                         .WithMany(p => p.Visitations)
                         .HasForeignKey(v => v.PatientId)
                         .HasConstraintName("FK_Visitation_Patient");

                     entity.HasOne(v => v.Doctor)
                         .WithMany(p => p.Visitations)
                         .HasForeignKey(v => v.DoctorId)
                         .HasConstraintName("FK_Visitation_Doctor");
                 });


            modelBuilder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(d => d.DiagnoseId);

                entity.Property(d => d.Name)
                    .IsUnicode(true)
                    .HasMaxLength(50);

                entity.Property(d => d.Comments)
                    .IsUnicode(true)
                    .HasMaxLength(250);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Diagnose_Patient");
            });

            modelBuilder.Entity<Medicament>(entity =>
                {
                    entity.HasKey(m => m.MedicamentId);

                    entity.Property(m => m.Name)
                            .IsRequired(true)
                            .IsUnicode(true)
                            .HasMaxLength(50);
                });


            modelBuilder.Entity<PatientMedicament>(entity =>
                {
                    entity.HasKey(p => new { p.PatientId, p.MedicamentId });

                    entity.HasOne(m => m.Medicament)
                        .WithMany(p => p.Prescriptions)
                        .HasForeignKey(m => m.MedicamentId);

                    entity.HasOne(m => m.Patient)
                        .WithMany(p => p.Prescriptions)
                        .HasForeignKey(m => m.PatientId);
                });
        }
    }
}
