using System;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Gaev.AnemicVsDDD.DDD;
using Xunit;

namespace Gaev.AnemicVsDDD.Tests.DDD
{
    public class CustomerRepositoryTests : TestBase
    {
        private static readonly Func<EquivalencyAssertionOptions<Customer>, EquivalencyAssertionOptions<Customer>>
            Options = opt => opt.IgnoringCyclicReferences().ExcludingFields();

        [Fact]
        public void It_should_insert()
        {
            // Given
            var repo = new CustomerRepository(Config.ConnectionString);
            var customer = RandomCustomer();

            // When
            repo.Save(customer);

            // Then
            var actual = repo.Load(customer.Id);
            actual.Should().BeEquivalentTo(customer, Options);
        }

        [Fact]
        public void It_should_update_and_insert()
        {
            // Given
            var repo = new CustomerRepository(Config.ConnectionString);
            var customer = RandomCustomer();
            repo.Save(customer);

            // When
            var item = new OrderItem(RandomString(), 10);
            customer.Order(item);
            repo.Save(customer);

            // Then
            var actual = repo.Load(customer.Id);
            actual.Should().BeEquivalentTo(customer, Options);
        }

        private static Customer RandomCustomer()
        {
            var customer = new Customer(RandomString(), 100);
            customer.Order(new OrderItem(RandomString(), 50));
            return customer;
        }
    }
}