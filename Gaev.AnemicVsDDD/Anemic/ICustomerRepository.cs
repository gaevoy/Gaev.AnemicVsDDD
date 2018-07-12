using System;

namespace Gaev.AnemicVsDDD.Anemic
{
    public interface ICustomerRepository
    {
        Customer Load(Guid id);
        void Save(Customer customer);
    }
}