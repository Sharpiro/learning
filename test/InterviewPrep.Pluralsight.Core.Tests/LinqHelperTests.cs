using System.Collections.Generic;
using System.IO;
using Xunit;
using InterviewPrep.LinqFundamentals;
using Newtonsoft.Json;
using InterviewPrep.LinqFundamentals.Models;
using System.Linq;

namespace InterviewPrep.Pluralsight.Core.Tests
{
    public class LinqHelperTests
    {
        private readonly LinqHelper _helper;
        private readonly string _movieData;
        private readonly string _testData;
        private readonly string _fileDirectory;

        public LinqHelperTests()
        {
            var directory = Directory.GetCurrentDirectory();
            _fileDirectory = $"{directory}/Data";
            _testData = File.ReadAllText($"{_fileDirectory}/TestData.json");
            _movieData = File.ReadAllText($"{_fileDirectory}/MovieData.json");
            _helper = new LinqHelper();
        }

        [Fact]
        public void GroupingTest()
        {

            var list = JsonConvert.DeserializeObject<List<TestData>>(_testData);
            _helper.Grouping(list);
        }

        [Fact]
        public void SwapTest()
        {
            var list1 = new List<int> { 1, 2, 3 };
            var list2 = new int[] { 1, 2, 3 };
            var list3 = _helper.DoStuff(list1);
            var list4 = _helper.DoStuff(list2);
        }

        [Fact]
        public void MovieLazyFailTest()
        {
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(_movieData).Select(m => new Movie { Year = m.Year });
            //Assert.Equal(movieList.Count(m => m.Year > 2000), movieList.Filter(m => m.Year > 2000).Count());
            foreach (var movie in movieList)
            {
                movie.Year = 1;
            }
            var firstMovieYear = movieList.FirstOrDefault().Year;
            Assert.False(firstMovieYear == 1);
        }

        [Fact]
        public void IEnumerableLazyFailTest()
        {
            TestListAndEnumerable.Test();
        }

        [Fact]
        public void ParseCsvTest()
        {
            var fuelDataPath = $"{_fileDirectory}/fuel.csv";
            var makeDataPath = $"{_fileDirectory}/manufacturers.csv";
            var fuelData = File.ReadAllLines(fuelDataPath).ParseCsv<Car>().ToList();
            var makeData = File.ReadAllLines(makeDataPath).ParseCsv<Manufacturer>().ToList();
            Assert.Equal(1205, fuelData.Count);
            Assert.Equal(43, makeData.Count);
        }

        [Fact]
        public void MostCommonCharacterTest()
        {
            var fuelDataPath = $"{_fileDirectory}/fuel.csv";
            var data = File.ReadAllLines(fuelDataPath).ParseCsv<Car>();
            var characters = data.SelectMany(d => d.Model);
            var groupedCharacters = characters.GroupBy(g => g)
                .Select(g => new { g.Key, Count = g.Count() })
                .OrderByDescending(o => o.Count);
            var mostCommonChar = groupedCharacters.Skip(1).FirstOrDefault();
            Assert.Equal('D', mostCommonChar.Key);
            Assert.Equal(618, mostCommonChar.Count);
        }

        [Fact]
        public void InnerJoinTest()
        {
            //return car name, manufacturer, and location
            var fuelDataPath = $"{_fileDirectory}/fuel.csv";
            var makeDataPath = $"{_fileDirectory}/manufacturers.csv";
            var carData = File.ReadAllLines(fuelDataPath).ParseCsv<Car>().ToList();
            var makeData = File.ReadAllLines(makeDataPath).ParseCsv<Manufacturer>().ToList();
            var temp = carData.Join(makeData, c => c.Make, m => m.Name, (c, m) => new
            {
                Name = c.Model,
                Manufacturer = m.Name,
                Location = m.Location
            });
            var firstEntry = temp.FirstOrDefault();
            Assert.Equal("4C", firstEntry.Name);
            Assert.Equal("ALFA ROMEO", firstEntry.Manufacturer);
            Assert.Equal("Italy", firstEntry.Location);
        }

        /// <summary>
        /// Get all of the passengers in all of the cars
        /// </summary>
        [Fact]
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
            Assert.Equal(3, carsAndpassengers.Count);
            Assert.Equal(5, passengers.Count);
        }
    }
}
