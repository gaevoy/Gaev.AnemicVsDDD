using System;

namespace Gaev.AnemicVsDDD.DDD
{
    public class CustomerController
    {
        private readonly ICustomerRepository _repo;

        public CustomerController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public Guid RegisterCustomer(string name, decimal funds)
        {
            var customer = new Customer(name, funds);
            _repo.Insert(customer);
            return customer.Id;
        }

        public Guid Order(Guid customerId, params OrderItem[] items)
        {
            var customer = _repo.Load(customerId);
            var order = customer.Order(items);
            _repo.Update(customer);
            return order.Id;
        }

        public void CancelOrder(Guid customerId, Guid orderId)
        {
            var customer = _repo.Load(customerId);
            customer.CancelOrder(customer.Orders.Find(e => e.Id == orderId));
            _repo.Update(customer);
        }
    }
}