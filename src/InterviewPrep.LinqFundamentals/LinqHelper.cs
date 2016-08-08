using System.Collections.Generic;
using System.Linq;
using InterviewPrep.LinqFundamentals.Models;

namespace InterviewPrep.LinqFundamentals
{
    public class LinqHelper
    {
        public void Grouping(IEnumerable<TestData> list)
        {
            //get average price for each item type
            //get average price per item type for item types with count larger than 2
            var averagePricePerItemType = list.GroupBy(td => td.Type)
                .Select(g => new
                {
                    Type = g.Key,
                    Sum = g.Sum(td => (double)td.Price) / g.Count()
                }).ToList();
        }

        public void Swap(IList<SwappableObject> list)
        {
            //for (var i = 0; i < list.Count; i++)
            //{
            //    var x = 0;
            //    var y = 1;
            //    Swap(ref x, ref y);
            //}
            foreach (var item in list)
            {
                Swap(item);
            }
            //var obj = list.FirstOrDefault();
            //Swap(ref obj.Value1, ref obj.Value2);
            //return list.Select(so => new SwappableObject { Value1 = so.Value2, Value2 = so.Value1 });
        }

        private void Swap(SwappableObject obj)
        {
            var temp = obj.Value1;
            obj.Value1 = obj.Value2;
            obj.Value2 = temp;
        }
    }
}
