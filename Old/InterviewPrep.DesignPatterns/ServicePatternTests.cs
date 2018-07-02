using Microsoft.VisualStudio.TestTools.UnitTesting;
using InterviewPrep.ServicePattern.BusinessLogic;
using InterviewPrep.ServicePattern.DataLayer.Entities;
using InterviewPrep.ServicePattern.DataLayer;
using Autofac;
using InterviewPrep.ServicePattern.PresentationLayer;

namespace InterviewPrep.DesignPatterns
{
    [TestClass]
    public class ServicePatternTests
    {
        private readonly IContainer _container;

        public ServicePatternTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AutoContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().AsSelf();
            builder.RegisterType<CarService>().AsSelf();
            builder.RegisterGeneric(typeof(GenericService<>)).AsSelf();
            builder.RegisterType<FakeController>().AsSelf();
            builder.RegisterType<FakeWorker>().AsSelf();
            builder.RegisterType<FakeService>().AsSelf();
            _container = builder.Build();
        }

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
            using (var scope = _container.BeginLifetimeScope())
            {
                var temp = scope.Resolve<AutoContext>();
                var temp2 = scope.Resolve<AutoContext>();
                var temp3 = scope.Resolve<CarService>().UnitOfWork.Context;
                var controller = scope.Resolve<FakeController>();
                var service = scope.Resolve<FakeService>();

                var hash1 = temp.GetHashCode();
                var hash2 = temp2.GetHashCode();
                var hash3 = temp3.GetHashCode();
                var hash4 = controller.GetData();
                var hash5 = service.Poll();
            }
            //var factory = new ContextFactory();
            var context = new AutoContext();
            var unitOfWork = new UnitOfWork(context);//give me this per request/process, and inject this instance into business services
            var carService = new CarService(unitOfWork);
            var manufacturerService = new GenericService<Manufacturer>(context);
            var ford = manufacturerService.Get(m => m.Name == "Ford");
            carService.AddCarToManfacturer("Test", ford.Id);
        }
    }
}
