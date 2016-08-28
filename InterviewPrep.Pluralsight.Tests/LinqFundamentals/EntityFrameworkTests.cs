using InterviewPrep.LinqFundamentals.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using InterviewPrep.LinqFundamentals.DataLayer.Entities;
using System.Data.Entity;

namespace InterviewPrep.Pluralsight.Tests.LinqFundamentals
{
    [TestClass]
    public class EntityFrameworkTests
    {
        [TestMethod]
        public void FindTop10FuelEfficientCarsTest()
        {
            var context = new CarContext();
            var topTen = context.Cars
                .Where(c => c.Make == "BMW")
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Model)
                .Skip(10)
                .Take(10)
                .Select(c => new { Model = c.Model })
                .ToList();
            context.Dispose();
        }

        [TestMethod]
        public void IQueryableTests()
        {
            var querier = new FakeDataSet<Car>();
            var count = querier.Count();
            //Extensions.Count(querier);
        }

        [TestMethod]
        public void Top2CarsByManufacturerTests()
        {
            var context = new CarContext();
            var topCars = context.Cars
                .GroupBy(c => c.Make)
                .Select(g => new
                {
                    Make = g.Key,
                    Cars = g.OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Model).Take(2)
                    .Select(c => new { c.Model, c.Combined })
                })
                .OrderByDescending(a => a.Cars.FirstOrDefault().Combined)
                .ToList();
        }
    }
}
