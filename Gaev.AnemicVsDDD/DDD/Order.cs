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

        public Guid Id { get; }
        public List<OrderItem> Items { get; }
        public Customer Customer { get; }
        public DateTimeOffset CreatedAt { get; }
    }
}