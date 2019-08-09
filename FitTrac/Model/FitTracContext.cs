using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FitTrac.Model
{
    public partial class FitTracContext : DbContext
    {
        public FitTracContext()
        {
        }

        public FitTracContext(DbContextOptions<FitTracContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Exercises> Exercises { get; set; }
        public virtual DbSet<Workouts> Workouts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:fittrac.database.windows.net,1433;Initial Catalog=FitTrac;Persist Security Info=False;User ID=Zefty;Password=Jaime123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Exercises>(entity =>
            {
                entity.HasKey(e => e.ExerciseId)
                    .HasName("PK__Exercise__A074AD2FF2C07F6F");

                entity.Property(e => e.ExerciseName).IsUnicode(false);

                entity.HasOne(d => d.Workout)
                    .WithMany(p => p.Exercises)
                    .HasForeignKey(d => d.WorkoutId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("WorkoutId");
            });

            modelBuilder.Entity<Workouts>(entity =>
            {
                entity.HasKey(e => e.WorkoutId)
                    .HasName("PK__Workouts__E1C42A01747EF515");

                entity.Property(e => e.WorkoutDescription).IsUnicode(false);

                entity.Property(e => e.WorkoutName).IsUnicode(false);
            });
        }
    }
}
