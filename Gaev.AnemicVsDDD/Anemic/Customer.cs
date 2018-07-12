using System;
using System.Collections.Generic;

namespace Gaev.AnemicVsDDD.Anemic
{
    public class Customer
    {
        public Guid Id { get; set; }
        public List<Order> Orders { get; set; }
        public string Name { get; set; }
        public decimal Funds { get; set; }
        public bool IsNew = true;
    }
}