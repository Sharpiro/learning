using System.Data.Entity;

namespace InterviewPrep.Generics
{
    public class CarContext : DbContext
    {
        public CarContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<CarContext>(null);
        }
    }
}
