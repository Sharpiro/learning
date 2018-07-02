using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Parallels.Tests
{
    [TestClass]
    public class DelayedServiceTests
    {
        [TestMethod]
        public async Task TestDelayedGet()
        {
            var service = new DelayedService();
            var list = new[] { Tuple.Create("A", 6), Tuple.Create("B", 4), Tuple.Create("C", 2), Tuple.Create("D", 7), Tuple.Create("E", 3), };
            var sortedList = list.OrderBy(i => i.Item2).ToList();
            var results = list.Select(async n => new { Name = n.Item1, Delay = await service.GetData(n.Item2) }).OrderByCompletion().ToList();

            for (var i = 0; i < results.Count; i++)
            {
                var resultTask = results[i];
                var result = await resultTask;

                Assert.AreEqual(sortedList[i].Item1, result.Name);
                Assert.AreEqual(sortedList[i].Item2, result.Delay);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task NullCheckTest()
        {
            await Do(null);
        }

        private Task Do(string param1)
        {
            return DoAsync(param1);
        }

        private async Task DoAsync(string param1)
        {
            if (param1 == null) throw new ArgumentNullException();
            await Task.Delay(1000);
            await Task.Yield();
        }
    }
}