using System.Data.Entity;
using InterviewPrep.ServicePattern.DataLayer.Entities;
using System.Diagnostics;

namespace InterviewPrep.ServicePattern.DataLayer
{
    public class AutoContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        public AutoContext() : base("AutoContext")
        {
            Database.SetInitializer<AutoContext>(null);
            Database.Log = l => Debug.WriteLine(l);
        }
    }
}