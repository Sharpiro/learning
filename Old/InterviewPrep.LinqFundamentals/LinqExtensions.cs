using System;
using System.Collections.Generic;
using System.Linq;

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
            //if (headers.Count() != objectProperties.Count())
            //    throw new ArgumentException("the # of headers must match # of properties", nameof(list));
            foreach (var item in list.Skip(1))
            {
                var row = item.Split(',');
                var entry = new T();
                foreach (var property in objectProperties)
                {
                    //var columnIndex = headers[property.Name];
                    int columnIndex;
                    var result = headers.TryGetValue(property.Name, out columnIndex);
                    if (!result)
                        continue;
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
        }
    }
}
