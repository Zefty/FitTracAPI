using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitTracAPI.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; }
        public int? WorkoutId { get; set; }
        public string ExerciseName { get; set; }
        public int? ExerciseReps { get; set; }
        public int? ExerciseSets { get; set; }

        [ForeignKey("WorkoutId")]
        [InverseProperty("Exercises")]
        public virtual Workout Workout { get; set; }
    }
}
