using InterviewPrep.DesignPatterns.DataLayer;

namespace InterviewPrep.DesignPatterns.Migrations
{
    using Entities;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SchoolContext context)
        {
            //  This method will be called after migrating to the latest version.

            var spanish = new Course { Name = "Spanish" };
            var english = new Course { Name = "English" };
            var history = new Course { Name = "History" };
            var math = new Course { Name = "Math" };

            var nancy = new Student { Name = "Nancy" };
            var dave = new Student { Name = "Dave" };
            var jack = new Student { Name = "Jack" };
            var dan = new Student { Name = "Dan" };

            context.Enrollments.AddOrUpdate(e => e.Id,
                new Enrollment { Course = spanish, Student = nancy, Grade = 45 },
                new Enrollment { Course = spanish, Student = dave, Grade = 15 },
                new Enrollment { Course = spanish, Student = jack, Grade = 55 },
                new Enrollment { Course = english, Student = dan, Grade = 60 },
                new Enrollment { Course = english, Student = jack, Grade = 20 },
                new Enrollment { Course = english, Student = dave, Grade = 77 },
                new Enrollment { Course = math, Student = dave, Grade = 80 },
                new Enrollment { Course = math, Student = jack, Grade = 40 },
                new Enrollment { Course = history, Student = nancy, Grade = 70 }
            );
        }
    }
}