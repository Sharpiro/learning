using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace InterviewPrep.LinqFundamentals
{
    public static class LinqExtensions
    {
        public static int CountX<T>(this IEnumerable<T> list)
        {
            var counter = 0;
            foreach (var item in list)
            {
                counter++;
            }
            return counter;
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> list, Func<T, bool> func)
        {
            foreach (var item in list)
            {
                if (func(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> ParseCsv<T>(this IEnumerable<string> list) where T : new()
        {
            var headers = list.FirstOrDefault().Split(',')
                .Select((h, i) => new { h, i })
                .ToDictionary(d => d.h, d => d.i);
            var objectProperties = typeof(T).GetProperties();
            if (headers.Count() != objectProperties.Count())
                throw new ArgumentException("the # of headers must match # of properties", nameof(list));
            foreach (var item in list.Skip(1))
            {
                var row = item.Split(',');
                var entry = new T();
                foreach (var property in objectProperties)
                {
                    var columnIndex = headers[property.Name];
                    var dataEntry = row[columnIndex];
                    if (property.PropertyType == typeof(int))
                    {
                        var temp2 = int.Parse(dataEntry);
                        property.SetValue(entry, temp2);
                    }
                    else if (property.PropertyType == typeof(float))
                    {
                        var temp2 = float.Parse(dataEntry);
                        property.SetValue(entry, temp2);
                    }
                    else
                        property.SetValue(entry, dataEntry);
                }
                yield return entry;
            }

            //return newList;
            //var newList = list.Skip(1).Where(s => !string.IsNullOrEmpty(s))
            //    .Select(s => s.Split(',')).Select(item => new EcoEntry
            //    {
            //        Year = int.Parse(item[0]),
            //        Make = item[1],
            //        Model = item[2],
            //        Liters = float.Parse(item[3]),
            //        Cylinders = int.Parse(item[4]),
            //        City = int.Parse(item[5]),
            //        Highway = int.Parse(item[6]),
            //        Combined = int.Parse(item[7])
            //    }).ToList();

            //return newList;
        }
    }

    public static class TestListAndEnumerable
    {
        public static void Test()
        {
            var b1 = GetBool1(); // returns false
            bool b2 = GetBool2(); // returns true
        }

        private static IEnumerable<BoolContainer> GetBool1()
        {
            IEnumerable<BoolContainer> list = new List<bool> { false }.Select(x => { Debug.WriteLine("Selecting!"); return new BoolContainer { Value = x }; });

            foreach (BoolContainer item in list)
            {
                item.Value = true;
            }

            return list;//list.Select(x => x.Value).First();
        }

        private static bool GetBool2()
        {
            List<BoolContainer> list = new List<bool> { false }.Select(x => { Debug.WriteLine("Selecting!"); return new BoolContainer { Value = x }; }).ToList();

            foreach (BoolContainer item in list)
            {
                item.Value = true;
            }

            return list.Select(x => x.Value).First();
        }

        private class BoolContainer
        {
            public bool Value { get; set; }
        }
    }
}
