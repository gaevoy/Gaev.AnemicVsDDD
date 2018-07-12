using System;
using System.Linq;

namespace Gaev.AnemicVsDDD.Anemic
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public Guid RegisterCustomer(string name, decimal funds)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = name,
                Funds = funds
            };
            _repo.Save(customer);
            return customer.Id;
        }

        public Guid Order(Guid customerId, params OrderItem[] items)
        {
            var customer = _repo.Load(customerId);
            if (customer.Funds < items.Sum(e => e.Cost))
                throw new Exception("Insufficient funds");
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.UtcNow,
                Items = items.ToList()
            };
            customer.Orders.Add(order);
            customer.Funds -= items.Sum(e => e.Cost);
            _repo.Save(customer);
            return order.Id;
        }

        public void CancelOrder(Guid customerId, Guid orderId)
        {
            var customer = _repo.Load(customerId);
            var order = customer.Orders.FirstOrDefault(e => e.Id == orderId);
            if (order == null)
                throw new Exception("Order is not found");
            customer.Orders.Remove(order);
            customer.Funds += order.Items.Sum(e => e.Cost);
            _repo.Save(customer);
        }
    }
}