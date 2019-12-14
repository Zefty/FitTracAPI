﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitTracAPI.Models;

namespace FitTracAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly FitTracAPIContext _context;
        private readonly ExercisesController _exercisesController;

        public WorkoutsController(FitTracAPIContext context, ExercisesController exercisesController)
        {
            _context = context;
            _exercisesController = exercisesController;
        }

        // GET: api/Workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkout()
        {
            return await _context.Workout.ToListAsync();
        }

        // GET: api/Workouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetWorkout(int id)
        {
            var workout = await _context.Workout.FindAsync(id);

            if (workout == null)
            {
                return NotFound();
            }

            return workout;
        }

        // PUT: api/Workouts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkout(int id, Workout workout)
        {
            if (id != workout.WorkoutId)
            {
                return BadRequest();
            }

            _context.Entry(workout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("EditWorkouts/")]
        public async Task<IActionResult> EditWorkouts(int id, Workout workout)
        {
            if (id != workout.WorkoutId)
            {
                return BadRequest();
            }




            _context.Entry(workout).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            foreach (Exercise exercise in workout.Exercises)
            {
                if (exercise.ExerciseId == 0)
                {
                    await _exercisesController.PostExercise(exercise);
                }
                else
                {
                    await _exercisesController.PutExercise(exercise.ExerciseId, exercise);
                }
            }

            //for (int i = 0; i < workouts.Exercises.Count; i++)
            //{
            //    if (workouts.Exercises.ElementAt(i).ExerciseId == 0)
            //    {
            //        await _exercisesController.PostExercises(exercises[i]);
            //    } else
            //    {
            //        await _exercisesController.PutExercises(exercises[i].ExerciseId, exercises[i]);
            //    }
            //}

            return NoContent();
        }

        // POST: api/Workouts
        [HttpPost]
        public async Task<ActionResult<Workout>> PostWorkout(Workout workout)
        {
            _context.Workout.Add(workout);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkout", new { id = workout.WorkoutId }, workout);
        }

        // DELETE: api/Workouts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Workout>> DeleteWorkout(int id)
        {
            var workout = await _context.Workout.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            _context.Workout.Remove(workout);
            await _context.SaveChangesAsync();

            return workout;
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workout.Any(e => e.WorkoutId == id);
        }
    }
}
