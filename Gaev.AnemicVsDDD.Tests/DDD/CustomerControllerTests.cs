using System;
using FluentAssertions;
using Gaev.AnemicVsDDD.DDD;
using NSubstitute;
using Xunit;

namespace Gaev.AnemicVsDDD.Tests.DDD
{
    public class CustomerControllerTests : TestBase
    {
        [Fact]
        public void It_should_register_customer()
        {
            // Given
            var repo = Substitute.For<ICustomerRepository>();
            var controller = new CustomerController(repo);
            var name = RandomString();
            var funds = RandomInt();

            // When
            var id = controller.RegisterCustomer(name, funds);

            // Then
            repo.Received().Save(Arg.Is<Customer>(e
                => e.Id == id && e.Name == name && e.Funds == funds));
        }

        [Fact]
        public void It_should_order_customer_items()
        {
            // Given
            var customer = new Customer("", 100);
            var repo = Substitute.For<ICustomerRepository>();
            repo.Load(customer.Id).Returns(customer);
            var controller = new CustomerController(repo);

            // When
            var item = new OrderItem("", 20);
            var orderId = controller.Order(customer.Id, item);

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
            var customer = new Customer("", 100);
            var repo = Substitute.For<ICustomerRepository>();
            repo.Load(customer.Id).Returns(customer);
            var controller = new CustomerController(repo);
            var item = new OrderItem("", 20);
            var orderId = controller.Order(customer.Id, item);

            // When
            controller.CancelOrder(customer.Id, orderId);

            // Then
            customer.Funds.Should().Be(100);
            customer.Orders.Should().BeEmpty();
            repo.Received().Save(customer);
        }
    }
}