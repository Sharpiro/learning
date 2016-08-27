using InterviewPrep.LinqFundamentals;
using InterviewPrep.LinqFundamentals.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Xunit;

namespace InterviewPrep.Pluralsight.Core.Tests.LinqFundamentals
{
    public class XmlHelperTests
    {
        private readonly string _fileDirectory;
        private readonly IReadOnlyList<Car> _carData;
        private readonly IReadOnlyList<Manufacturer> _makeData;

        public XmlHelperTests()
        {
            _fileDirectory = $"{Directory.GetCurrentDirectory()}/LinqFundamentals/Data";
            _carData = File.ReadAllLines($"{_fileDirectory}/fuel.csv").ParseCsv<Car>().ToList();
            _makeData = File.ReadAllLines($"{_fileDirectory}/manufacturers.csv").ParseCsv<Manufacturer>().ToList();
        }

        [Fact]
        public void CreateXDocumentTest()
        {
            var xmlHelper = new XmlHelper(_carData);
            var data = xmlHelper.CreateXDocument();
            Assert.False(string.IsNullOrEmpty(data.ToString()));
        }

        [Fact]
        public void QueryXDocumentTest()
        {
            var xmlHelper = new XmlHelper(_carData);
            var xdata = xmlHelper.CreateXDocument();
            var car = xdata.Element("Cars").Elements("Car")
                .Where(d => d.Attribute("Manufacturer").Value.ToLowerInvariant() == "bmw");

            using (var reader = new StringReader(xdata.ToString()))
            {
                using (var xReader = XmlReader.Create(reader))
                {
                    while (xReader.Read())
                    {

                    }
                }
            }
        }
    }
}