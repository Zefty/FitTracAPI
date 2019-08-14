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
    public class ExercisesControllerUnitTests
    {
        public static readonly DbContextOptions<FitTracContext> options
        = new DbContextOptionsBuilder<FitTracContext>()
            .UseInMemoryDatabase(databaseName: "testDatabase")
            .Options;

        public static readonly IList<Exercises> exercises = new List<Exercises>
        {
            new Exercises()
            {
                ExerciseName = "Pushups",
                ExerciseReps = 10,
                ExerciseSets = 3,
                WorkoutId = 1
            },
            new Exercises()
            {
                ExerciseName = "Squats",
                ExerciseReps = 10,
                ExerciseSets = 4,
                WorkoutId = 2

            }
        };

        [TestInitialize]
        public void SetupDb()
        {
            using (var context = new FitTracContext(options))
            {
                // populate the db
                context.Exercises.Add(exercises[0]);
                context.Exercises.Add(exercises[1]);
                context.SaveChanges();
            }
        }

        [TestCleanup]
        public void ClearDb()
        {
            using (var context = new FitTracContext(options))
            {
                // clear the db
                context.Exercises.RemoveRange(context.Exercises);
                context.SaveChanges();
            };
        }

        [TestMethod]
        public async Task TestGetExercisesSuccessfully()
        {
            using (var context = new FitTracContext(options))
            {
                ExercisesController exercisesController = new ExercisesController(context);
                ActionResult<IEnumerable<Exercises>> result = await exercisesController.GetExercises();

                Assert.IsNotNull(result);

                // convert everything into an array for easier access
                var resultArray = result.Value.ToArray();
                var exercisesArray = exercises.ToArray();

                // compare first object of array
                Assert.AreEqual(resultArray[0].ExerciseName, exercisesArray[1].ExerciseName);
                Assert.AreEqual(resultArray[0].ExerciseReps, exercisesArray[1].ExerciseReps);
                Assert.AreEqual(resultArray[0].ExerciseSets, exercisesArray[1].ExerciseSets);
                Assert.AreEqual(resultArray[0].ExerciseId, exercisesArray[1].ExerciseId);
                Assert.AreEqual(resultArray[0].WorkoutId, exercisesArray[1].WorkoutId);

                // compare second object of array 
                Assert.AreEqual(resultArray[1].ExerciseName, exercisesArray[0].ExerciseName);
                Assert.AreEqual(resultArray[1].ExerciseReps, exercisesArray[0].ExerciseReps);
                Assert.AreEqual(resultArray[1].ExerciseSets, exercisesArray[0].ExerciseSets);
                Assert.AreEqual(resultArray[1].ExerciseId, exercisesArray[0].ExerciseId);
                Assert.AreEqual(resultArray[1].WorkoutId, exercisesArray[0].WorkoutId);


            }

        }

        [TestMethod]
        public async Task TestPutExercisesSuccessfully()
        {
            using (var context = new FitTracContext(options))
            {
                string updateExerciseName = "Update name of exercise";
                Exercises exercise1 = context.Exercises.Where(x => x.ExerciseName == exercises[0].ExerciseName).Single();
                exercise1.ExerciseName = updateExerciseName;

                ExercisesController exercisesController = new ExercisesController(context);
                IActionResult result = await exercisesController.PutExercises(exercise1.ExerciseId, exercise1) as IActionResult;

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(NoContentResult));
            }
        }

        [TestMethod]
        public async Task TestGetFilteredExerciseExercisesSuccessfully()
        {
            using (var context = new FitTracContext(options))
            {
                ExercisesController exercisesController = new ExercisesController(context);
                ActionResult<Exercises> result = await exercisesController.GetFilteredExercises(1);

                Assert.IsNotNull(result);
            }
        }




    }
}
