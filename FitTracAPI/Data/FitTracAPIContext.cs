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
                           .HasOne(p => p.Workout)
                           .WithMany(b => b.Exercises)
                           .OnDelete(DeleteBehavior.Cascade)
                           .IsRequired();
        }

        public DbSet<FitTracAPI.Models.Exercise> Exercise { get; set; }

        public DbSet<FitTracAPI.Models.Workout> Workout { get; set; }
    }
}
