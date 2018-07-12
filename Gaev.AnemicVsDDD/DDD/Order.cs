using System;
using System.Collections.Generic;
using System.Linq;

namespace Gaev.AnemicVsDDD.DDD
{
    public class Order
    {
        public Order(Customer customer, OrderItem[] items)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            Customer = customer;
            Items = items.ToList();
        }

        public Guid Id { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public Customer Customer { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public decimal TotalCost => Items.Sum(e => e.Cost);
        internal bool IsNew = true;

        private Order()
        {
            //EF requires that a parameterless constructor be declared
        }
    }
}