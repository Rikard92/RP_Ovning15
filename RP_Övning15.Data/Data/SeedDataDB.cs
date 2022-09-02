using Bogus;
using Microsoft.Extensions.DependencyInjection;
using RP_Ovning15.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_Ovning15.Data.Data
{
    public class SeedDataDB
    {
        private static LmsApiContext db = default;

        public static async Task SeedTheData(LmsApiContext context, IServiceProvider services)
        {
            if (context == null) throw new ArgumentNullException(nameof(db));

            db = context;

            ArgumentNullException.ThrowIfNull(nameof(services));

            //EnsureDeleted(db);
            //if (db.Module.Count() < 1)
            //{
            //    var Modules = GetModules();
            //    await db.AddRangeAsync(Modules);
            //}
            if (db.Course.Count() < 1)
            {
                var Courses = GetCourses();
                await db.AddRangeAsync(Courses);
            }
             await db.SaveChangesAsync();
        }

        private static void EnsureDeleted(LmsApiContext db)
        {
            db.Database.EnsureDeleted();
        }

        private static IEnumerable<Module> GetModules()
        {
            var faker = new Faker("sv");

            var Modules = new List<Module>();

            for (int i = 0; i < 3; i++)
            {
                var temp = new Module
                {
                    Title = faker.Hacker.Verb(),
                    StartDate = DateTime.Now.AddDays(faker.Random.Int(-5, 5))
                    //,Modules = new List<Module>()
                };

                Modules.Add(temp);
            }

            return Modules;
        }

        private static IEnumerable<Course> GetCourses()
        {
            var faker = new Faker("sv");

            var Courses = new List<Course>();

            for (int i = 0; i < 20; i++)
            {
                var temp = new Course
                {
                    Title = faker.Hacker.Verb(),
                    StartDate = DateTime.Now.AddDays(faker.Random.Int(-5, 5)),
                    Modules = (ICollection<Module>)GetModules()
                };

                Courses.Add(temp);
            }

            return Courses;
        }
    }
}
