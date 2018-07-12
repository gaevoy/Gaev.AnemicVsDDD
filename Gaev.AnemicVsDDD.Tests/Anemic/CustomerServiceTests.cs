using System;
using System.Collections.Generic;
using FluentAssertions;
using Gaev.AnemicVsDDD.Anemic;
using NSubstitute;
using Xunit;

namespace Gaev.AnemicVsDDD.Tests.Anemic
{
    public class CustomerServiceTests : TestBase
    {
        [Fact]
        public void It_should_register_customer()
        {
            // Given
            var repo = Substitute.For<ICustomerRepository>();
            var service = new CustomerService(repo);
            var name = RandomString();
            var funds = RandomInt();

            // When
            var id = service.RegisterCustomer(name, funds);

            // Then
            repo.Received().Save(Arg.Is<Customer>(e
                => e.Id == id && e.Name == name && e.Funds == funds));
        }

        [Fact]
        public void It_should_order_customer_items()
        {
            // Given
            var customerId = Guid.NewGuid();
            var customer = new Customer
            {
                Id = customerId,
                Funds = 100,
                Orders = new List<Order>()
            };
            var repo = Substitute.For<ICustomerRepository>();
            repo.Load(customerId).Returns(customer);
            var service = new CustomerService(repo);

            // When
            var item = new OrderItem {Cost = 20};
            var orderId = service.Order(customerId, item);

            // Then
            customer.Funds.Should().Be(80);
            var order = customer.Orders[0];
            order.Id.Should().Be(orderId);
            order.Items.Should().Contain(item);
            repo.Received().Save(customer);
        }

        [Fact]
        public void It_should_cancel_customer_order()
        {
            // Given
            var customerId = Guid.NewGuid();
            var orderId = Guid.NewGuid();
            var customer = new Customer
            {
                Id = customerId,
                Funds = 80,
                Orders = new List<Order>
                {
                    new Order
                    {
                        Id = orderId,
                        Items = new List<OrderItem> {new OrderItem {Cost = 20}}
                    }
                }
            };
            var repo = Substitute.For<ICustomerRepository>();
            repo.Load(customerId).Returns(customer);
            var service = new CustomerService(repo);

            // When
            service.CancelOrder(customerId, orderId);

            // Then
            customer.Funds.Should().Be(100);
            customer.Orders.Should().BeEmpty();
            repo.Received().Save(customer);
        }
    }
}