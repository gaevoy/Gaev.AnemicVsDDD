using System;
using Microsoft.EntityFrameworkCore;

namespace Gaev.AnemicVsDDD.DDD
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Customer Load(Guid id)
        {
            using (var session = OpenSession())
                return session.Set<Customer>().Find(id);
        }

        public void Insert(Customer customer)
        {
            using (var session = OpenSession())
            {
                session.Set<Customer>().Add(customer);
                session.SaveChanges();
            }
        }

        public void Update(Customer customer)
        {
            using (var session = OpenSession())
            {
                session.Set<Customer>().Attach(customer);
                session.SaveChanges();
            }
        }

        private DbContext OpenSession()
            => new DbContext(new DbContextOptionsBuilder().UseSqlServer(_connectionString).Options);
    }
}