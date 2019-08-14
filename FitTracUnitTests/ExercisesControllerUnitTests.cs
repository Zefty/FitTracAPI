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
            },
            new Exercises()
            {
                ExerciseName = "Squats",
                ExerciseReps = 10,
                ExerciseSets = 4,

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

    }
}
