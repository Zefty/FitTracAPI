using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FitTracAPI.Models
{
    public class Workout
    {
        public Workout()
        {
            Exercises = new HashSet<Exercise>();
        }

        public int WorkoutId { get; set; }
/*        [Required]
        [StringLength(255)]*/
        public string WorkoutName { get; set; }
/*        [Required]
        [StringLength(255)]*/
        public string WorkoutDescription { get; set; }
        public bool IsFavourite { get; set; }

        [InverseProperty("Workout")]
        public virtual ICollection<Exercise> Exercises { get; set; }

        public static implicit operator Workout(ActionResult<IEnumerable<Workout>> v)
        {
            throw new NotImplementedException();
        }
    }
}
