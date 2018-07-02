using InterviewPrep.EncapsulatedDL;
using InterviewPrep.EncapsulatedDL.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.DesignPatterns.EncapsulatedDL
{
    public class RepositoryTests
    {
        public void GetAllTest()
        {
            var customers = new List<Customer> { new Customer { Id = 1, Name = "David" } }.AsQueryable();
            var mockContext = new Mock<CommerceContext>();
            //var mockDbSet = GenericSetupQueryableMockSet
            //mockContext.Setup(c => c.Customers.AsNoTracking()).Returns(null);
        }
    }
}
