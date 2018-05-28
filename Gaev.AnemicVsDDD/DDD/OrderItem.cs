namespace Gaev.AnemicVsDDD.DDD
{
    public class OrderItem
    {
        public OrderItem(string product, decimal cost)
        {
            Product = product;
            Cost = cost;
        }

        public string Product { get; }
        public decimal Cost { get; }
    }
}