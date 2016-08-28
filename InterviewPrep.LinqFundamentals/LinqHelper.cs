using System.Collections.Generic;
using System.Linq;
using InterviewPrep.LinqFundamentals.Models;

namespace InterviewPrep.LinqFundamentals
{
    public static class TestDataExtensions
    {
        public static void Grouping(this IEnumerable<TestData> list)
        {
            //get average price for each item type
            //get average price per item type for item types with count larger than 2
            var averagePricePerItemType = list.GroupBy(td => td.Type).Where(g => g.Count() > 2)
                .Select(g => new
                {
                    Type = g.Key,
                    Sum = g.Sum(td => (double)td.Price) / g.Count()
                }).ToList();
        }
    }
}