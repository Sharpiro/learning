using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
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
            var list = new[] { "A", "B", "C", "D", "E" };
            var results = list.Select(async n => new { Name = n, Delay = await service.GetData(n) }).ToList();
            
            foreach (var resultTask in results)
            {
                var result = await resultTask;
                Debug.WriteLine($"{result.Name}, {result.Delay}");
            }
        }
    }
}