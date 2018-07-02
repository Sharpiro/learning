using InterviewPrep.EncapsulatedDL.Entities;
using System.Collections.Generic;

namespace InterviewPrep.EncapsulatedDL
{
    public class UnitOfWork
    {
        private Repository<Customer> _customers;
        private Repository<Order> _orders;

        public Repository<Customer> Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new Repository<Customer>();
                return _customers;
            }
        }
        public Repository<Order> Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new Repository<Order>();
                return _orders;
            }
        }
    }

    public class CustomerService
    {
        private readonly Repository<Customer> _customers;

        public Repository<Customer> Customers { get { return _customers; } }

        public CustomerService(Repository<Customer> customers)
        {
            _customers = customers;
        }

        public IEnumerable<Customer> GetCustomersWithLongName()
        {
            return _customers.GetAll(c => c.Name.Length > 10);
        }
    }
}