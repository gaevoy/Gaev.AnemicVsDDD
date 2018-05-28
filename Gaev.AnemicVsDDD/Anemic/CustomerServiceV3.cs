using System;
using System.Linq;

namespace Gaev.AnemicVsDDD.Anemic
{
    public class CustomerServiceV3
    {
        private readonly ICustomerRepository _repo;
        private readonly ISystem _system;

        public CustomerServiceV3(ICustomerRepository repo, ISystem system)
        {
            _repo = repo;
            _system = system;
        }

        public Customer RegisterCustomer(string name, decimal funds)
        {
            var customer = new Customer
            {
                Id = _system.NewGuid(),
                Name = name,
                Funds = funds
            };
            _repo.Insert(customer);
            return customer;
        }

        public Order Order(Guid customerId, params OrderItem[] items)
        {
            var customer = _repo.Load(customerId);
            if (customer.Funds < items.Sum(e => e.Cost))
                throw new Exception("Insufficient funds");
            var order = new Order
            {
                Id = _system.NewGuid(),
                CreatedAt = _system.GetUtcNow(),
                Items = items.ToList()
            };
            customer.Orders.Add(order);
            customer.Funds -= items.Sum(e => e.Cost);
            _repo.Update(customer);
            return order;
        }

        public void CancelOrder(Guid customerId, Guid orderId)
        {
            var customer = _repo.Load(customerId);
            var order = customer.Orders.FirstOrDefault(e => e.Id == orderId);
            if (order == null)
                throw new Exception("Order is not found");
            customer.Orders.Remove(order);
            customer.Funds += order.Items.Sum(e => e.Cost);
            _repo.Update(customer);
        }
    }
}