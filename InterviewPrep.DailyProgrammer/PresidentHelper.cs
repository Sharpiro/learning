using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using InterviewPrep.Core;

namespace InterviewPrep.DailyProgrammer
{
    public class PresidentHelper
    {
        private readonly string _path;

        public PresidentHelper(string path)
        {
            _path = path;
        }

        public int Find()
        {
            var presidents = ReadPresidentsFile().ToList();
            var maxAliveYear = 0;
            var maxAlive = 0;
            for (var i = 1732; i < 2016; i++)
            {
                var aliveCounter = 0;
                foreach (var president in presidents)
                {
                    var isAlive = president.IsAlive(i);
                    if (isAlive)
                        aliveCounter++;
                }
                if (aliveCounter <= maxAlive) continue;
                maxAlive = aliveCounter;
                maxAliveYear = i;
            }
            return maxAliveYear;
        }

        private IEnumerable<President> ReadPresidentsFile()
        {
            var dictionaryList = CsvReader.ReadFile(_path);
            var presidents = new List<President>();
            var properites = typeof(President).GetProperties();
            foreach (var dict in dictionaryList)
            {
                var president = new President();
                foreach (var propertyInfo in properites)
                {
                    var attrValue = propertyInfo.CustomAttributes.FirstOrDefault(ca => ca.AttributeType.Name == "DisplayNameAttribute")?
                        .ConstructorArguments.FirstOrDefault().Value.ToString();
                    var propertyName = propertyInfo.Name;
                    var dataName = attrValue ?? propertyName;
                    object value;
                    dict.TryGetValue(dataName.ToLower(), out value);
                    if (value == null) continue;
                    var property = typeof(President).GetProperty(propertyName);
                    if (property.PropertyType.FullName.ToLower().Contains("datetime"))
                    {
                        var dateString = (string)value;
                        value = string.IsNullOrEmpty(dateString) ? null : (DateTime?)DateTime.Parse(dateString);
                    }
                    property.SetValue(president, value);
                }
                presidents.Add(president);
            }
            return presidents;
        }
    }

    public class President
    {
        [DisplayName("president")]
        public string Name { get; set; }
        [DisplayName("birth date")]
        public DateTime? BirthDate { get; set; }
        [DisplayName("birth place")]
        public string BirthPlace { get; set; }
        [DisplayName("death date")]
        public DateTime? DeathDate { get; set; }
        [DisplayName("location of death")]
        public string DeathLocation { get; set; }

        public bool IsAlive(int year)
        {
            try
            {
                if (year >= BirthDate.Value.Year && year <= DeathDate.Value.Year)
                {
                    return true;
                }
            }
            catch (InvalidOperationException)
            {
            }
            return false;
        }
    }
}
