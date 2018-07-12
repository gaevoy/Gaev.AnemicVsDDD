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


        public Guid Id { get; private set; }
        public List<Order> Orders { get; private set; }
        public string Name { get; private set; }
        public decimal Funds { get; private set; }
        internal bool IsNew = true;

        public Order FindOrder(Guid orderId)
        {
            return Orders.Find(e => e.Id == orderId);
        }

        public Order Order(params OrderItem[] items)
        {
            if (Funds < items.Sum(e => e.Cost))
                throw new Exception("Insufficient funds");
            var order = new Order(this, items);
            Orders.Add(order);
            Funds -= order.TotalCost;
            return order;
        }

        public void CancelOrder(Order order)
        {
            if (order == null || !Orders.Contains(order))
                throw new Exception("Order is not found");
            Orders.Remove(order);
            Funds += order.TotalCost;
        }

        private Customer()
        {
            //EF requires that a parameterless constructor be declared
        }
    }
}