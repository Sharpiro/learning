using InterviewPrep.Concepts.ChangeTracking;
using InterviewPrep.Concepts.ChangeTracking.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.Concepts.Tests
{
    [TestClass]
    public class ChangeTrackerTests
    {
        [TestMethod]
        public void ChangeTest()
        {
            var context = new AutoContext();
            var tracker = new ChangeTracker(context);
            //var car = tracker.GetTrackedObject<Car>();
            var car = new Car { Id = 2, Name = "Taurus" };
            //car.Manufacturer = new Manufacturer { Name = "Ford" };
            tracker.TrackObject(car);
            car.Name = "Changed";
            tracker.CheckChanges();
        }
    }
}
