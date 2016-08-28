using System.Collections.Generic;
using System.IO;
using InterviewPrep.LinqFundamentals;
using InterviewPrep.LinqFundamentals.Models;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using InterviewPrep.LinqFundamentals.DataLayer.Entities;

namespace InterviewPrep.Pluralsight.Core.Tests
{
    [TestClass]
    public class LinqHelperTests
    {
        private readonly string _movieData;
        private readonly string _testData;
        private readonly string _fileDirectory;
        private readonly List<CarModel> _carData;
        private readonly List<Manufacturer> _makeData;

        public LinqHelperTests()
        {
            _fileDirectory = $"{Directory.GetCurrentDirectory()}/Data";
            _testData = File.ReadAllText($"{_fileDirectory}/TestData.json");
            _movieData = File.ReadAllText($"{_fileDirectory}/MovieData.json");
            _carData = File.ReadAllLines($"{_fileDirectory}/fuel.csv").ParseCsv<CarModel>().ToList();
            _makeData = File.ReadAllLines($"{_fileDirectory}/manufacturers.csv").ParseCsv<Manufacturer>().ToList();
        }

        [TestMethod]
        public void MovieLazyFailTest()
        {
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(_movieData)
                .Select(m => new Movie { Year = m.Year });
            //Assert.AreEqual(movieList.Count(m => m.Year > 2000), movieList.Filter(m => m.Year > 2000).Count());
            foreach (var movie in movieList)
            {
                movie.Year = 1;
            }
            var firstMovieYear = movieList.FirstOrDefault().Year;
            Assert.IsFalse(firstMovieYear == 1);
        }

        [TestMethod]
        public void ParseCsvTest()
        {
            Assert.AreEqual(1164, _carData.Count);
            Assert.AreEqual(42, _makeData.Count);
        }

        [TestMethod]
        public void MostCommonCharacterTest()
        {
            var characters = _carData.SelectMany(d => d.Model);
            var groupedCharacters = characters.GroupBy(g => g)
                .Select(g => new { g.Key, Count = g.Count() })
                .OrderByDescending(o => o.Count);
            var mostCommonChar = groupedCharacters.Skip(1).FirstOrDefault();
            Assert.AreEqual('D', mostCommonChar.Key);
            Assert.AreEqual(594, mostCommonChar.Count);
        }

        [TestMethod]
        public void InnerJoinTest()
        {
            var temp = _carData.Join(_makeData, c => c.Make, m => m.Name, (c, m) => new
            {
                Name = c.Model,
                Manufacturer = m.Name,
                Location = m.Location,
                FuelEconomy = c.Combined
            });
            var firstEntry = temp.FirstOrDefault();
            Assert.AreEqual("4C", firstEntry.Name);
            Assert.AreEqual("ALFA ROMEO", firstEntry.Manufacturer);
            Assert.AreEqual("Italy", firstEntry.Location);
        }

        [TestMethod]
        public void GroupingTest()
        {
            var temp = _carData.GroupBy(c => new { c.Make, c.Model }).Where(g => g.Count() > 1);
            var top2ByManufactuer = _carData.GroupBy(c => c.Make.ToLower())
                .Select(g => new
                {
                    Manufacturer = g.Key,
                    Cars = g.OrderByDescending(c => c.Combined).Take(2).ToList()
                }).OrderBy(e => e.Manufacturer).ToList();
            var previousManufacturer = 'A';
            foreach (var topTwo in top2ByManufactuer)
            {
                var firstCombined = topTwo.Cars.FirstOrDefault().Combined;
                var secondCombined = topTwo.Cars.LastOrDefault().Combined;
                Assert.IsTrue(secondCombined <= firstCombined);
                var firstLetterOfMake = topTwo.Manufacturer.FirstOrDefault();
                Assert.IsTrue(previousManufacturer <= firstLetterOfMake);
                previousManufacturer = firstLetterOfMake;
            }
        }

        [TestMethod]
        public void GroupJoinTest()
        {
            var top2ByManufactuer = _makeData.OrderBy(m => m.Name)
                .Join(_carData, m => m.Name.ToLower(), c => c.Make.ToLower(),
                (m, c) => new
                {
                    Car = c,
                    Manufacturer = m
                })
                .GroupBy(c => c.Manufacturer.Name.ToLower())
                .Select(g => new
                {
                    Manufacturer = g.FirstOrDefault().Manufacturer,
                    Cars = g.OrderByDescending(obj => obj.Car.Combined).Select(obj => obj.Car).Take(2).ToList()
                }).ToList();

            var temp = _makeData.OrderBy(m => m.Name)
                .GroupJoin(_carData, m => m.Name.ToLower(), c => c.Make.ToLower(),
                (m, cars) => new
                {
                    Manufacturer = m,
                    Cars = cars.OrderByDescending(c => c.Combined)
                }).ToList();
            var previousManufacturer = 'A';
            foreach (var topTwo in top2ByManufactuer)
            {
                var firstCombined = topTwo.Cars.FirstOrDefault().Combined;
                var secondCombined = topTwo.Cars.LastOrDefault().Combined;
                Assert.IsTrue(secondCombined <= firstCombined);
                var firstLetterOfMake = topTwo.Manufacturer.Name.FirstOrDefault();
                Assert.IsTrue(previousManufacturer <= firstLetterOfMake);
                previousManufacturer = firstLetterOfMake;
                Assert.IsNotNull(topTwo.Manufacturer.Location);
            }
            previousManufacturer = 'A';
            foreach (var topTwo in temp)
            {
                var firstCombined = topTwo.Cars.FirstOrDefault().Combined;
                var secondCombined = topTwo.Cars.LastOrDefault().Combined;
                Assert.IsTrue(secondCombined <= firstCombined);
                var firstLetterOfMake = topTwo.Manufacturer.Name.FirstOrDefault();
                Assert.IsTrue(previousManufacturer <= firstLetterOfMake);
                previousManufacturer = firstLetterOfMake;
                Assert.IsNotNull(topTwo.Manufacturer.Location);
            }
        }

        [TestMethod]
        public void Top3FuelEfficientCarsByCountryTest()
        {
            var temp = _makeData.Join(_carData, m => m.Name, c => c.Make,
                (m, c) => new
                {
                    Manufacturer = m,
                    Car = c

                }).GroupBy(obj => obj.Manufacturer.Location)
                .Select(g => new
                {
                    Location = g.Key,
                    Cars = g.OrderByDescending(c => c.Car.Combined).Select(obj => obj.Car).Take(3).ToList()
                }).OrderByDescending(r => r.Cars.Max(c => c.Combined)).ToList();
        }

        [TestMethod]
        public void AggregateTest()
        {
            var temp = _carData.Aggregate(new CarStatistics(),
                (acc, c) => acc.Next(c), acc => acc.Compute());
        }

        /// <summary>
        /// Get all of the passengers in all of the cars
        /// </summary>
        [TestMethod]
        public void SelectManyTest()
        {
            var cars = new List<CarHolder>
            {
                new CarHolder
                {
                    Id = 1,
                    Name = "car1",
                    Passengers = new List<Passenger>
                    {
                        new Passenger {Id = 1, Name = "person1" },
                        new Passenger {Id = 1, Name = "person2" }
                    }
                },
                new CarHolder
                {
                    Id = 2,
                    Name = "car2",
                    Passengers = new List<Passenger>
                    {
                        new Passenger {Id = 3, Name = "person3" }
                    }
                },
                new CarHolder
                {
                    Id = 3,
                    Name = "car3",
                    Passengers = new List<Passenger>
                    {
                        new Passenger {Id = 4, Name = "person4" },
                        new Passenger {Id = 5, Name = "person5" }
                    }
                }
            };

            //list of cars w/ list of passengers
            var carsAndpassengers = cars.Select(c => c.Passengers).ToList();
            var passengers = cars.SelectMany(c => c.Passengers).ToList();
            Assert.AreEqual(3, carsAndpassengers.Count);
            Assert.AreEqual(5, passengers.Count);
        }
    }
}