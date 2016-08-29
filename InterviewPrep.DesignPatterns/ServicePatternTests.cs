using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewPrep.ServicePattern.BusinessLogic;
using InterviewPrep.ServicePattern.DataLayer.Entities;
using InterviewPrep.ServicePattern.DataLayer;

namespace InterviewPrep.DesignPatterns
{
    [TestClass]
    public class ServicePatternTests
    {
        [TestMethod]
        public void GetCarsTest()
        {
            //var factory = new ContextFactory();
            //var service = new GenericService<Car>(factory);
            //var cars = service.GetAll();
            //cars = service.GetAll();
            //cars = service.GetAll();
            //cars = service.GetAll();
            //service = new GenericService<Car>(factory);
            //cars = service.GetAll();
            //cars = service.GetAll();
            //cars = service.GetAll();
            //var service2 = new CarService(factory);
            //var cars2 = service2.GetAllByManufacturer("Ford");
        }

        [TestMethod]
        public void ChangeCarsTest()
        {
            var factory = new ContextFactory();
            var context = new AutoContext();
            var unitOfWork = new UnitOfWork(context);//give me this per request/process, and inject this instance into business services
            var carService = new CarService(unitOfWork);
            var manufacturerService = new GenericService<Manufacturer>(context);
            var ford = manufacturerService.Get(m => m.Name == "Ford");
            carService.AddCarToManfacturer("Test", ford.Id);
        }
    }
}
