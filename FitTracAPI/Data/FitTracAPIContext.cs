using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FitTracAPI.Models;

namespace FitTracAPI.Models
{
    public class FitTracAPIContext : DbContext
    {
        public FitTracAPIContext (DbContextOptions<FitTracAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>()
                           .HasOne(d => d.Workout)
                           .WithMany(p => p.Exercises)
                           .HasForeignKey(d => d.WorkoutId)
                           .OnDelete(DeleteBehavior.Cascade)
                           .HasConstraintName("WorkoutId");
        }

        public DbSet<FitTracAPI.Models.Exercise> Exercise { get; set; }

        public DbSet<FitTracAPI.Models.Workout> Workout { get; set; }
    }
}
