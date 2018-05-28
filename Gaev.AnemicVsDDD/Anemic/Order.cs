using System;
using System.Collections.Generic;

namespace Gaev.AnemicVsDDD.Anemic
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public Customer Customer { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}