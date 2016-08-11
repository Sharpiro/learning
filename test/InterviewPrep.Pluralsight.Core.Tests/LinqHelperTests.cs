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
        public LinqHelperTests()
        {
            var directory = Directory.GetCurrentDirectory();
            var fileDirectory = $"{directory}/Data";
            _testData = File.ReadAllText($"{fileDirectory}/TestData.json");
            _movieData = File.ReadAllText($"{fileDirectory}/MovieData.json");
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
        public void MovieTest()
        {
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(_movieData);
            Assert.Equal(movieList.Count(m => m.Year > 2000), movieList.Filter(m => m.Year > 2000).Count());
        }
    }
}
