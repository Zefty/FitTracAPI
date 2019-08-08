using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitTrac.Model
{
    public partial class Exercises
    {
        public int ExerciseId { get; set; }
        public int? WorkoutId { get; set; }
        [Required]
        [StringLength(255)]
        public string ExerciseName { get; set; }
        public int? ExerciseReps { get; set; }
        public int? ExerciseSets { get; set; }

        [ForeignKey("WorkoutId")]
        [InverseProperty("Exercises")]
        public virtual Workouts Workout { get; set; }
    }
}
