namespace InterviewPrep.ServicePattern.Migrations
{
    using DataLayer.Entities;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataLayer.AutoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataLayer.AutoContext context)
        {
            context.Manufacturers.AddOrUpdate(m => m.Name,
                new Manufacturer { Name = "Ford", Cars = new List<Car> { new Car { Name = "Taurus" } } },
                new Manufacturer { Name = "Chevrolet", Cars = new List<Car> { new Car { Name = "Tahoe" } } },
                new Manufacturer { Name = "Jeep", Cars = new List<Car> { new Car { Name = "Grand Cherokee" } } }
                );
        }
    }
}
