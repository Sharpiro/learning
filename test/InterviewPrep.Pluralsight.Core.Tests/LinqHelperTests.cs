using System.Collections.Generic;
using System.IO;
using Xunit;
using InterviewPrep.LinqFundamentals;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using InterviewPrep.LinqFundamentals.Models;
using System.Linq;

namespace InterviewPrep.Pluralsight.Core.Tests
{
    public class LinqHelperTests
    {
        private readonly string _jsonData;
        public LinqHelperTests()
        {
            var directory = Directory.GetCurrentDirectory();
            var filePath = $"{directory}/Data/TestData.json";
            _jsonData = File.ReadAllText(filePath);
        }

        [Fact]
        public void GroupingTest()
        {

            var list = JsonConvert.DeserializeObject<List<TestData>>(_jsonData);
            var helper = new LinqHelper();
            helper.Grouping(list);
        }

        [Fact]
        public void SwapTest()
        {

            var list = new List<SwappableObject>
            {
                new SwappableObject {Value1 = 1, Value2 = 2},
                new SwappableObject {Value1 = 3, Value2 = 4},
                new SwappableObject {Value1 = 5, Value2 = 6},
                new SwappableObject {Value1 = 7, Value2 = 8},
            };
            list.CountX();
            var helper = new LinqHelper();
            helper.Swap(list);
            Assert.Equal(2, list[0].Value1);
            Assert.Equal(3, list[1].Value2);
            Assert.Equal(6, list[2].Value1);
            Assert.Equal(7, list[3].Value2);
        }
    }
}
