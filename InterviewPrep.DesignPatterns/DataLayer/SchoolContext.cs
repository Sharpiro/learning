using InterviewPrep.DesignPatterns.Entities;
using System.Data.Entity;
using System.Diagnostics;

namespace InterviewPrep.DesignPatterns.DataLayer
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public SchoolContext()
        {
            //Database.SetInitializer<SchoolContext>(null);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SchoolContext>());
            Database.Log = s => Debug.WriteLine(s);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Course>().Property(p => p.Name).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}