using System.Collections.Generic;

namespace InterviewPrep.EncapsulatedDL.Entities
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}