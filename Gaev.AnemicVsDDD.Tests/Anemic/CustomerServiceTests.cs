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
            var funds = Random.Next();

            // When
            var id = service.RegisterCustomer(name, funds);

            // Then
            repo.Received().Insert(Arg.Is<Customer>(e 
                => e.Id == id && e.Name == name && e.Funds == funds));
        }
    }
}