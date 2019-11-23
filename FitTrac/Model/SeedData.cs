using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitTrac.Model
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FitTracContext(
                serviceProvider.GetRequiredService<DbContextOptions<FitTracContext>>()))
            {
                if (context.Workouts.Count() > 0)
                {
                    return;
                }

                context.Workouts.AddRange(
                    new Workouts
                    {
                        WorkoutId = 0,
                        WorkoutName = "Seed Workout",
                        WorkoutDescription = "Seed Workout",
                        IsFavourite = false,
                        Exercises = null, 
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
