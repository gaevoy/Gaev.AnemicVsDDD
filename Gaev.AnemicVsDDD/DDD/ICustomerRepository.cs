using System;

namespace Gaev.AnemicVsDDD.DDD
{
    public interface ICustomerRepository
    {
        Customer Load(Guid id);
        void Save(Customer customer);
    }
}