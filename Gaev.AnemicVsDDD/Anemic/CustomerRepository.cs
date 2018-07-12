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
            {
                var customer = session.Customers
                    .Include(e => e.Orders)
                    .ThenInclude(e => e.Items)
                    .FirstOrDefault(e => e.Id == id);
                MarkAsSaved(customer);
                return customer;
            }
        }

        public void Save(Customer customer)
        {
            using (var session = new DbSession(_connectionString))
            {
                session.Entry(customer).State = customer.IsNew ? EntityState.Added : EntityState.Modified;
                foreach (var order in customer.Orders)
                {
                    session.Entry(order).State = order.IsNew ? EntityState.Added : EntityState.Modified;
                    foreach (var item in order.Items)
                        session.Entry(item).State = item.IsNew ? EntityState.Added : EntityState.Modified;
                }

                session.SaveChanges();
                MarkAsSaved(customer);
            }
        }

        private static void MarkAsSaved(Customer customer)
        {
            if (customer == null) return;
            customer.IsNew = false;
            foreach (var order in customer.Orders)
            {
                order.IsNew = false;
                foreach (var item in order.Items)
                    item.IsNew = false;
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