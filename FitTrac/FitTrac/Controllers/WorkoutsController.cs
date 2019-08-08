﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitTrac.Model;

namespace FitTrac.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly FitTracContext _context;

        public WorkoutsController(FitTracContext context)
        {
            _context = context;
        }

        // GET: api/Workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workouts>>> GetWorkouts()
        {
            return await _context.Workouts.ToListAsync();
        }

        // GET: api/Workouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workouts>> GetWorkouts(int id)
        {
            var workouts = await _context.Workouts.FindAsync(id);

            if (workouts == null)
            {
                return NotFound();
            }

            return workouts;
        }

        // PUT: api/Workouts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkouts(int id, Workouts workouts)
        {
            if (id != workouts.WorkoutId)
            {
                return BadRequest();
            }

            _context.Entry(workouts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutsExists(id))
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

        // POST: api/Workouts
        [HttpPost]
        public async Task<ActionResult<Workouts>> PostWorkouts(Workouts workouts)
        {
            _context.Workouts.Add(workouts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkouts", new { id = workouts.WorkoutId }, workouts);
        }

        // DELETE: api/Workouts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Workouts>> DeleteWorkouts(int id)
        {
            var workouts = await _context.Workouts.FindAsync(id);
            if (workouts == null)
            {
                return NotFound();
            }

            _context.Workouts.Remove(workouts);
            await _context.SaveChangesAsync();

            return workouts;
        }

        private bool WorkoutsExists(int id)
        {
            return _context.Workouts.Any(e => e.WorkoutId == id);
        }
    }
}
