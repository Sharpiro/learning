using InterviewPrep.OdeToFoodCore.Entities;
using Microsoft.Data.Entity;

namespace InterviewPrep.OdeToFoodCore.DataAccess
{
    public class FoodContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }

        public FoodContext()
        {

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}