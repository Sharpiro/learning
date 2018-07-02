using InterviewPrep.LinqFundamentals;
using InterviewPrep.LinqFundamentals.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace InterviewPrep.Pluralsight.Core.Tests.LinqFundamentals
{
    [TestClass]
    public class XmlHelperTests
    {
        private readonly string _fileDirectory;
        private readonly IReadOnlyList<CarModel> _carData;
        private readonly IReadOnlyList<Manufacturer> _makeData;

        public XmlHelperTests()
        {
            _fileDirectory = $"{Directory.GetCurrentDirectory()}/LinqFundamentals/Data";
            _carData = File.ReadAllLines($"{_fileDirectory}/fuel.csv").ParseCsv<CarModel>().ToList();
            _makeData = File.ReadAllLines($"{_fileDirectory}/manufacturers.csv").ParseCsv<Manufacturer>().ToList();
        }

        [TestMethod]
        public void CreateXDocumentTest()
        {
            var xmlHelper = new XmlHelper(_carData);
            var data = xmlHelper.CreateXDocument();
            Assert.IsFalse(string.IsNullOrEmpty(data.ToString()));
        }

        [TestMethod]
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