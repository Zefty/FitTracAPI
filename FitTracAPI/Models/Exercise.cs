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
        public string ExerciseName { get; set; }
        public string ExerciseReps { get; set; }
        public string ExerciseSets { get; set; }

        [ForeignKey("WorkoutId")]
        public int? WorkoutId { get; set; }
        [InverseProperty("Exercises")]
        public virtual Workout Workout { get; set; }
    }
}
