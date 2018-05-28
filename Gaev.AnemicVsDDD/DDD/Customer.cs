using System;
using System.Collections.Generic;
using System.Linq;

namespace Gaev.AnemicVsDDD.DDD
{
    public class Customer
    {
        public Customer(string name, decimal funds)
        {
            Id = Guid.NewGuid();
            Orders = new List<Order>();
            Name = name;
            Funds = funds;
        }

        public Guid Id { get; }
        public List<Order> Orders { get; }
        public string Name { get; }
        public decimal Funds { get; private set; }

        public Order Order(params OrderItem[] items)
        {
            if (Funds < items.Sum(e => e.Cost))
                throw new Exception("Insufficient funds");
            var order = new Order(this, items);
            Orders.Add(order);
            Funds -= items.Sum(e => e.Cost);
            return order;
        }

        public void CancelOrder(Order order)
        {
            if (order == null || !Orders.Contains(order))
                throw new Exception("Order is not found");
            Orders.Remove(order);
            Funds += order.Items.Sum(e => e.Cost);
        }
    }
}