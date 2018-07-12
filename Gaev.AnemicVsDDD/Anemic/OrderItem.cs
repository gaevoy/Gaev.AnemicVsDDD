using System;

namespace Gaev.AnemicVsDDD.Anemic
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public decimal Cost { get; set; }
        public bool IsNew = true;
    }
}