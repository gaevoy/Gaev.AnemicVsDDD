using System;

namespace Gaev.AnemicVsDDD.Anemic
{
    public interface ICustomerRepository
    {
        Customer Load(Guid id);
        void Insert(Customer customer);
        void Update(Customer customer);
    }
}