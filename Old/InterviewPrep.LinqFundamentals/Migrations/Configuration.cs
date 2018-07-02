namespace InterviewPrep.LinqFundamentals.Migrations
{
    using DataLayer.Entities;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InterviewPrep.LinqFundamentals.DataLayer.CarContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InterviewPrep.LinqFundamentals.DataLayer.CarContext context)
        {
            var cars = File.ReadAllLines($"C:/temp/fuel.csv").ParseCsv<Car>().ToArray();
            context.Cars.AddOrUpdate(c => new { c.Make, c.Model, c.Liters, c.Cylinders, c.City, c.Highway, c.Combined }, cars);
        }
    }
}
