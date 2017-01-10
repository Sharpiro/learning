using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallels.Tests
{
    [TestClass]
    public class GetterTests
    {
        //this test always succeeds
        [TestMethod]
        public void ResultTest()
        {
            for (var i = 0; i < 500; i++)
            {
                var currentThreadId = Thread.CurrentThread.ManagedThreadId;

                var instantThreadId = ThreadGetter.GetThreadIdInstant().Result;
                var delayedThreadId = ThreadGetter.GetThreadIdDelayed().Result;
                var forcedNewThreadId = ThreadGetter.GetThreadIdForcedNew().Result;

                Assert.AreEqual(currentThreadId, instantThreadId);
                Assert.AreNotEqual(currentThreadId, delayedThreadId);
                Assert.AreNotEqual(currentThreadId, forcedNewThreadId);
            }
        }

        //mixed results
        [TestMethod]
        public async Task AwaitDelayedTest()
        {

            for (var i = 0; i < 500; i++)
            {
                try
                {
                    var currentThreadId = Thread.CurrentThread.ManagedThreadId;

                    var delayedThreadId = await ThreadGetter.GetThreadIdDelayed();

                    Assert.AreNotEqual(currentThreadId, delayedThreadId);
                }
                catch (Exception ex)
                {
                    throw new Exception($"failed at iteration: {i}", ex);
                }
            }

        }

        //mixed results
        [TestMethod]
        public async Task AwaitForcedNewTest()
        {
            for (var i = 0; i < 500; i++)
            {
                try
                {
                    var currentThreadId = Thread.CurrentThread.ManagedThreadId;

                    var forcedNewThreadId = await ThreadGetter.GetThreadIdForcedNew();

                    Assert.AreNotEqual(currentThreadId, forcedNewThreadId);
                }
                catch (Exception ex)
                {
                    throw new Exception($"failed at iteration: {i}", ex);
                }
            }
        }
    }

    public static class ThreadGetter
    {
        public static async Task<int> GetThreadIdInstant() => Thread.CurrentThread.ManagedThreadId;

        public static async Task<int> GetThreadIdDelayed()
        {
            await Task.Yield();
            await Task.Delay(1);
            return Thread.CurrentThread.ManagedThreadId;
        }

        public static async Task<int> GetThreadIdForcedNew() => await Task.Run(() => Thread.CurrentThread.ManagedThreadId);
    }
}