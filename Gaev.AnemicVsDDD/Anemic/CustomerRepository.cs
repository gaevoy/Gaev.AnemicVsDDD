using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Gaev.AnemicVsDDD.Anemic
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
            using (var session = new DbSession(_connectionString))
                return session.Customers
                    .Include(e => e.Orders)
                    .ThenInclude(e => e.Items)
                    .FirstOrDefault(e => e.Id == id);
        }

        public void Insert(Customer customer)
        {
            using (var session = new DbSession(_connectionString))
            {
                session.Customers.Add(customer);
                session.SaveChanges();
            }
        }

        public void Update(Customer customer)
        {
            using (var session = new DbSession(_connectionString))
            {
                session.Entry(customer).State = EntityState.Modified;
                foreach (var order in customer.Orders)
                {
                    session.Entry(order).State = EntityState.Modified;
                    foreach (var item in order.Items)
                        session.Entry(item).State = EntityState.Modified;
                }

                session.SaveChanges();
            }
        }


        class DbSession : DbContext
        {
            public DbSession(string connectionString) :
                base(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
            {
            }

            public DbSet<Customer> Customers { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderItem> OrderItems { get; set; }
        }
    }
}