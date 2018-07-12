using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Gaev.AnemicVsDDD.Anemic;
using Xunit;

namespace Gaev.AnemicVsDDD.Tests.Anemic
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
        public void It_should_update()
        {
            // Given
            var repo = new CustomerRepository(Config.ConnectionString);
            var customer = RandomCustomer();
            repo.Save(customer);

            // When
            customer.Name = RandomString();
            customer.Funds = RandomInt();
            customer.Orders[0].CreatedAt = DateTimeOffset.UtcNow.AddHours(1);
            customer.Orders[0].Items[0].Product = RandomString();
            customer.Orders[0].Items[0].Cost = RandomInt();
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
            customer.Name = RandomString();
            customer.Orders.Add(new Order
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.UtcNow,
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        Product = RandomString(),
                        Cost = RandomInt()
                    }
                }
            });
            repo.Save(customer);

            // Then
            var actual = repo.Load(customer.Id);
            actual.Should().BeEquivalentTo(customer, Options);
        }

        private static Customer RandomCustomer()
        {
            return new Customer
            {
                Id = Guid.NewGuid(),
                Name = RandomString(),
                Funds = RandomInt(),
                Orders = new List<Order>
                {
                    new Order
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTimeOffset.UtcNow,
                        Items = new List<OrderItem>
                        {
                            new OrderItem
                            {
                                Id = Guid.NewGuid(),
                                Product = RandomString(),
                                Cost = RandomInt()
                            }
                        }
                    }
                }
            };
        }
    }
}