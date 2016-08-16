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
        private readonly string _movieData;
        private readonly string _testData;
        private readonly string _fileDirectory;
        private readonly List<Car> _carData;
        private readonly List<Manufacturer> _makeData;

        public LinqHelperTests()
        {
            _fileDirectory = $"{Directory.GetCurrentDirectory()}/Data";
            _testData = File.ReadAllText($"{_fileDirectory}/TestData.json");
            _movieData = File.ReadAllText($"{_fileDirectory}/MovieData.json");
            _carData = File.ReadAllLines($"{_fileDirectory}/fuel.csv").ParseCsv<Car>().ToList();
            _makeData = File.ReadAllLines($"{_fileDirectory}/manufacturers.csv").ParseCsv<Manufacturer>().ToList();
        }

        [Fact]
        public void GroupingTest()
        {

            //var list = JsonConvert.DeserializeObject<List<TestData>>(_testData);
            //list.Grouping();
        }

        [Fact]
        public void MovieLazyFailTest()
        {
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(_movieData)
                .Select(m => new Movie { Year = m.Year });
            //Assert.Equal(movieList.Count(m => m.Year > 2000), movieList.Filter(m => m.Year > 2000).Count());
            foreach (var movie in movieList)
            {
                movie.Year = 1;
            }
            var firstMovieYear = movieList.FirstOrDefault().Year;
            Assert.False(firstMovieYear == 1);
        }

        [Fact]
        public void ParseCsvTest()
        {
            Assert.Equal(1205, _carData.Count);
            Assert.Equal(43, _makeData.Count);
        }

        [Fact]
        public void MostCommonCharacterTest()
        {
            var characters = _carData.SelectMany(d => d.Model);
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
            var temp = _carData.Join(_makeData, c => c.Make, m => m.Name, (c, m) => new
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