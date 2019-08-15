using System;
using System.Collections.Generic;
using System.Text;
using FitTrac.Controllers;
using FitTrac.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace FitTracUnitTests
{
    [TestClass]
    public class WorkoutsControllerUnitTests
    {
        public static readonly DbContextOptions<FitTracContext> options 
        = new DbContextOptionsBuilder<FitTracContext>()
            .UseInMemoryDatabase(databaseName: "testDatabase")
            .Options;

        private static readonly ICollection<Exercises> exercises = new List<Exercises>
        {
             new Exercises()
             {
                 ExerciseId = 1,
                 ExerciseName = "Exercise 1",
                 ExerciseReps = 5,
                 ExerciseSets = 5,
             }
        };


        public static readonly IList<Workouts> workouts = new List<Workouts>
        {
            new Workouts()
            {
                WorkoutName = "Workout 1",
                WorkoutDescription = "Beginner Workout!",
                IsFavourite = true,
            },
            new Workouts()
            {
                WorkoutName = "Workout 2",
                WorkoutDescription = "Advanced Workout!",
                IsFavourite = false,

            }
        };

        [TestInitialize]
        public void SetupDb()
        {
            using (var context = new FitTracContext(options))
            {
                // populate the db
                context.Workouts.Add(workouts[0]);
                context.Workouts.Add(workouts[1]);
                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void ClearDb()
        {
            using (var context = new FitTracContext(options))
            {
                // clear the db
                context.Workouts.RemoveRange(context.Workouts);
                context.SaveChanges();
            };
        }

        [TestMethod]
        public async Task TestGetWorkoutsSuccessfully()
        {
            using (var context = new FitTracContext(options))
            {
                ExercisesController exercisesController = new ExercisesController(context);
                WorkoutsController workoutsController = new WorkoutsController(context, exercisesController);
                ActionResult<IEnumerable<Workouts>> result = await workoutsController.GetWorkouts();

                // check if get method acutally returns something 
                Assert.IsNotNull(result);

                // convert everything into an array for easier access
                var resultArray = result.Value.ToArray();
                var workoutsArray = workouts.ToArray();

                // compare first object of array
                Assert.AreEqual(resultArray[0].WorkoutName, workoutsArray[0].WorkoutName);
                Assert.AreEqual(resultArray[0].WorkoutDescription, workoutsArray[0].WorkoutDescription);
                Assert.AreEqual(resultArray[0].WorkoutId, workoutsArray[0].WorkoutId);

                // compare second object of array 
                Assert.AreEqual(resultArray[1].WorkoutName, workoutsArray[1].WorkoutName);
                Assert.AreEqual(resultArray[1].WorkoutDescription, workoutsArray[1].WorkoutDescription);
                Assert.AreEqual(resultArray[1].WorkoutId, workoutsArray[1].WorkoutId);
            }

        }

        //[TestMethod]
        //public async Task TestPostWorkoutSuccessfully()
        //{
        //    using (var context = new FitTracContext(options))
        //    {
        //        ICollection<Exercises> testExercise = new List<Exercises>();
        //        Workouts testWorkout = new Workouts()
        //        {
        //            WorkoutId = 1,
        //            WorkoutName = "Test Post",
        //            WorkoutDescription = "Successful?",
        //            IsFavourite = true,
        //            Exercises = testExercise,
        //        };

        //        ExercisesController exercisesController = new ExercisesController(context);
        //        WorkoutsController workoutsController = new WorkoutsController(context, exercisesController);
        //        var result = await workoutsController.PostWorkouts(testWorkout);

        //        Assert.IsNotNull(result);
        //    }
        //}

        [TestMethod]
        public async Task TestPutWorkoutSuccessfully()
        {
            using (var context = new FitTracContext(options))
            {
                string updateWorkoutName = "Update name of workout";
                Workouts workouts1 = context.Workouts.Where(x => x.WorkoutName == workouts[0].WorkoutName).Single();
                workouts1.WorkoutName = updateWorkoutName;

                ExercisesController exercisesController = new ExercisesController(context);
                WorkoutsController workoutsController = new WorkoutsController(context, exercisesController);
                IActionResult result = await workoutsController.PutWorkouts(workouts1.WorkoutId, workouts1) as IActionResult;

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(NoContentResult));
            }


        }

        [TestMethod]
        public async Task TestEditWorkoutSuccessfully()
        {
            using (var context = new FitTracContext(options))
            {
                string updateWorkoutName = "Update name of workout";
                Workouts workouts1 = context.Workouts.Where(x => x.WorkoutName == workouts[0].WorkoutName).Single();
                workouts1.WorkoutName = updateWorkoutName;

                string updateExerciseName = "Update name of exercise";
                ICollection<Exercises> tempExercises = new List<Exercises>
                {
                     new Exercises()
                     {
                         ExerciseId = 1,
                         ExerciseName = updateExerciseName,
                         ExerciseReps = 5,
                         ExerciseSets = 5,
                     }
                };

                workouts1.Exercises = tempExercises;

                ExercisesController exercisesController = new ExercisesController(context);
                WorkoutsController workoutsController = new WorkoutsController(context, exercisesController);
                IActionResult result = await workoutsController.EditWorkouts(workouts1.WorkoutId, workouts1) as IActionResult;

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(NoContentResult));

            }
        }


    }
}
