using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parallels.CustomTask;
using System.Threading;
using System.Threading.Tasks;

namespace Parallels.Tests
{
    [TestClass]
    public class CustomAwaiterTests
    {
        [TestMethod]
        public async Task CustomAwaitableTest()
        {
            var awaitable = new CustomAwaitable();
            string result = await awaitable;

            Assert.AreEqual(CustomAwaiter.Message, result);
        }

        [TestMethod]
        public async Task NonAsyncTaskTest()
        {
            var x = await Do();

            Assert.AreEqual(1, x);
        }

        private Task<int> Do()
        {
            Thread.Sleep(2000);
            return Task.FromResult(1);
        }
    }
}
