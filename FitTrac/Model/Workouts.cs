using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace FitTrac.Model
{
    public partial class Workouts
    {
        public Workouts()
        {
            Exercises = new HashSet<Exercises>();
        }

        public int WorkoutId { get; set; }
        [Required]
        [StringLength(255)]
        public string WorkoutName { get; set; }
        [Required]
        [StringLength(255)]
        public string WorkoutDescription { get; set; }
        public bool IsFavourite { get; set; }

        [InverseProperty("Workout")]
        public virtual ICollection<Exercises> Exercises { get; set; }

        public static implicit operator Workouts(ActionResult<IEnumerable<Workouts>> v)
        {
            throw new NotImplementedException();
        }
    }
}
