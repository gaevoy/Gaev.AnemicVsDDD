using System;
using System.Collections.Generic;
using FluentAssertions;
using Gaev.AnemicVsDDD.Anemic;
using Xunit;

namespace Gaev.AnemicVsDDD.Tests.Anemic
{
    public class CustomerRepositoryTests : TestBase
    {
        [Fact]
        public void It_should_insert()
        {
            // Given
            var repo = new CustomerRepository(Config.ConnectionString);
            var customer = RandomCustomer();

            // When
            repo.Insert(customer);

            // Then
            var actual = repo.Load(customer.Id);
            actual.Should().BeEquivalentTo(customer, opt => opt.IgnoringCyclicReferences());
        }

        [Fact]
        public void It_should_update()
        {
            // Given
            var repo = new CustomerRepository(Config.ConnectionString);
            var customer = RandomCustomer();
            repo.Insert(customer);

            // When
            customer.Name = RandomString();
            customer.Funds = Random.Next();
            customer.Orders[0].CreatedAt = DateTimeOffset.UtcNow.AddHours(1);
            customer.Orders[0].Items[0].Product = RandomString();
            customer.Orders[0].Items[0].Cost = Random.Next();
            repo.Update(customer);

            // Then
            var actual = repo.Load(customer.Id);
            actual.Should().BeEquivalentTo(customer, opt => opt.IgnoringCyclicReferences());
        }

        private static Customer RandomCustomer()
        {
            return new Customer
            {
                Id = Guid.NewGuid(),
                Name = RandomString(),
                Funds = Random.Next(),
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
                                Cost = Random.Next()
                            }
                        }
                    }
                }
            };
        }
    }
}