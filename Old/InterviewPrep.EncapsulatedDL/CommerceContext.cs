using InterviewPrep.EncapsulatedDL.Entities;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace InterviewPrep.EncapsulatedDL
{
    public class CommerceContext : DbContext
    {
        private readonly SqlProviderServices _sqlProviderInstance;

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public CommerceContext() : base("DefaultConnection")
        {
            _sqlProviderInstance = SqlProviderServices.Instance;
        }
    }
}