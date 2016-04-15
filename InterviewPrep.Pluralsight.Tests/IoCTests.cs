using InterviewPrep.Generics.Entities;
using InterviewPrep.Generics.IocContainer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.Pluralsight.Tests
{
    [TestClass]
    public class IoCTests
    {
        [TestMethod]
        public void Can_Resolve_Types()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlLogger>();

            var logger = ioc.Resolve<ILogger>();

            Assert.AreEqual(typeof(SqlLogger), logger.GetType());
        }

        [TestMethod]
        public void Can_Resolve_Types_Default_Constructor()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlLogger>();
            ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

            var repository = ioc.Resolve<IRepository<Employee>>();

            Assert.AreEqual(typeof(SqlRepository<Employee>), repository.GetType());
        }

        [TestMethod]
        public void Can_Resolve_Concrete_Type()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlLogger>();
            ioc.For<IRepository<Employee>>().Use<SqlRepository<Employee>>();

            var service = ioc.Resolve<InvoiceService>();

            Assert.IsNotNull(service);
        }

        /// <summary>
        /// convert from unbound generic type to closed constructed type
        /// </summary>
        [TestMethod]
        public void Can_Resolve_Unbound_Generics()
        {
            var ioc = new Container();
            ioc.For<ILogger>().Use<SqlLogger>();
            ioc.For(typeof(IRepository<>)).Use(typeof(SqlRepository<>));

            var service = ioc.Resolve<InvoiceService>();

            Assert.IsNotNull(service);
        }
    }
}