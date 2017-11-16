namespace P01_StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data.Models;

    public class StudentSystemContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        public StudentSystemContext()
        {
        }

        public StudentSystemContext(DbContextOptions options)
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
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(p => p.CourseId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(80);

                entity.Property(p => p.Description)
                    .IsUnicode();

                entity.Property(p => p.StartDate)
                    .IsRequired()
                    .HasColumnType("DATETIME2");

                entity.Property(p => p.EndDate)
                    .IsRequired()
                    .HasColumnType("DATETIME2");

                entity.Property(p => p.Price)
                    .IsRequired();
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(p => p.StudentId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);

                entity.Property(p => p.PhoneNumber)
                    .IsUnicode(false)
                    .HasColumnType("CHAR(10)");

                entity.Property(p => p.Birthday)
                    .HasColumnType("DATETIME2");

                entity.Property(p => p.RegisteredOn)
                    .IsRequired()
                    .HasColumnType("DATETIME2");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasKey(p => p.ResourceId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);

                entity.Property(p => p.Url)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(p => p.ResourceType)
                    .IsRequired();

                entity.HasOne(c => c.Course)
                    .WithMany(r => r.Resources)
                    .HasForeignKey(c => c.CourseId);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasKey(p => p.HomeworkId);

                entity.Property(p => p.Content)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(p => p.ContentType)
                    .IsRequired();

                entity.Property(p => p.SubmissionTime)
                    .IsRequired()
                    .HasColumnType("DATETIME2");

                entity.HasOne(s => s.Student)
                    .WithMany(h => h.HomeworkSubmissions)
                    .HasForeignKey(s => s.StudentId);

                entity.HasOne(c => c.Course)
                    .WithMany(h => h.HomeworkSubmissions)
                    .HasForeignKey(c => c.CourseId);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(p => new { p.StudentId, p.CourseId });

                entity.HasOne(c => c.Course)
                    .WithMany(s => s.StudentsEnrolled)
                    .HasForeignKey(c => c.CourseId);

                entity.HasOne(c => c.Student)
                    .WithMany(s => s.CourseEnrollments)
                    .HasForeignKey(c => c.StudentId);
            });
        }
    }
}
