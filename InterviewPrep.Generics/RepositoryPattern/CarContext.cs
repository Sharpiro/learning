using InterviewPrep.Generics.Entities;
using System.Data.Entity;

namespace InterviewPrep.Generics.RepositoryPattern
{
    public class CarContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Model> Models { get; set; }

        public CarContext()
        {
            //Database.SetInitializer<CarContext>(null);
            Database.SetInitializer(new DropCreateDatabaseAlways<CarContext>());
        }
    }
}
