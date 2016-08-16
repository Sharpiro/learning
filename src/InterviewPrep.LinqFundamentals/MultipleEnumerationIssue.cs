using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace InterviewPrep.LinqFundamentals
{
    public static class MultipleEnumerationIssue
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
