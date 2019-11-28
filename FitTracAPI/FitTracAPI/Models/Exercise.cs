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
/*        [Required]
        [StringLength(255)]*/
        public string ExerciseName { get; set; }
        public int? ExerciseReps { get; set; }
        public int? ExerciseSets { get; set; }

        [ForeignKey("WorkoutId")]
        [InverseProperty("Exercises")]
        public virtual Workout Workout { get; set; }
    }
}
