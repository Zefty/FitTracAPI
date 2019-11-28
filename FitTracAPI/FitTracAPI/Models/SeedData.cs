using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitTracAPI.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FitTracAPIContext(
                serviceProvider.GetRequiredService<DbContextOptions<FitTracAPIContext>>()))
            {
                // Look for any movies.
                if (context.Workout.Count() > 0 && context.Exercise.Count() > 0)
                {
                    return;   // DB has been seeded
                }

                context.Workout.AddRange(
                    new Workout
                    {
                        WorkoutId = 0,
                        WorkoutName = "Seed Workout",
                        WorkoutDescription = "Seed Workout",
                        IsFavourite = false,
                        Exercises = null,
                    }
                );


                context.Exercise.AddRange(
                    new Exercise
                    {
                        ExerciseId = 0,
                        ExerciseName = "Seed Exercise",
                        ExerciseReps = 0,
                        ExerciseSets = 0,
                        WorkoutId = 0,
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
