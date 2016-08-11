using System.Collections.Generic;
using System.IO;
using Xunit;
using InterviewPrep.LinqFundamentals;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InterviewPrep.LinqFundamentals.Models;
using System.Linq;
using System;

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
            var data = File.ReadAllLines(fuelDataPath).ParseCsvEcoData();
        }
    }
}
