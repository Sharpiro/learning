using InterviewPrep.LinqFundamentals.DataLayer.Entities;
using System.Data.Entity;
using System.Diagnostics;

namespace InterviewPrep.LinqFundamentals.DataLayer
{
    public class CarContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public CarContext() : base("CarContext")
        {
            Database.SetInitializer<CarContext>(null);
            Database.Log = l => Debug.WriteLine(l);
        }
    }
}