using System;

namespace Gaev.AnemicVsDDD.DDD
{
    public class OrderItem
    {
        public OrderItem(string product, decimal cost)
        {
            Id = Guid.NewGuid();
            Product = product;
            Cost = cost;
        }

        public Guid Id { get; private set; }
        public string Product { get; private set; }
        public decimal Cost { get; private set; }
        internal bool IsNew = true;

        private OrderItem()
        {
            //EF requires that a parameterless constructor be declared
        }
    }
}