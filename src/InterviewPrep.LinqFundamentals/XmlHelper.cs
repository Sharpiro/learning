using InterviewPrep.LinqFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace InterviewPrep.LinqFundamentals
{
    public class XmlHelper
    {
        private readonly IReadOnlyCollection<Car> _records;

        public XmlHelper(IReadOnlyCollection<Car> records)
        {
            _records = records;
        }

        public XDocument CreateXDocument()
        {
            var document = new XDocument(new XElement("Cars",
                _records.Select(r => new XElement("Car",
                    new XAttribute("Name", r.Model),
                    new XAttribute("Combined", r.Combined),
                    new XAttribute("Manufacturer", r.Make)
                ))));
            return document;
        }

        public T ParseXML<T>()
        {
            throw new NotImplementedException();
        }
    }
}