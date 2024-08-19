using Banking.Domain.Entities.Customers;
using Banking.SharedKernel.Error;

namespace Domain.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_Create_ShouldReturnObjectInstance()
        {
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@bank.com";
            var ssn = "416-27-7825";

            var custormer = Customer.Create(firstName, lastName, email, ssn);

            Assert.True(custormer.IsSuccess);
            Assert.IsType<Customer>(custormer.Value);
            Assert.NotEmpty(custormer.Value.Id.ToString());
            Assert.Equal(firstName, custormer.Value.FirstName);
            Assert.Equal(lastName, custormer.Value.LastName);
            Assert.Equal(email, custormer.Value.Email);
            Assert.Equal(ssn, custormer.Value.SSN.Value);
        }

        [Theory]
        [InlineData("", "Doe", "john.doe@bank.com", "416-27-7825")]
        [InlineData("John", null, "john.doe@bank.com", "416-27-7825")]
        [InlineData("John", "Doe", " ", "416-27-7825")]
        [InlineData("John", "Doe", "john.doe@bank.com", "")]
        public void Customer_Create_ShouldThrowAnException(string firstName, string lastName, string email, string ssn)
        {
            var customer = Customer.Create(firstName, lastName, email, ssn);

            Assert.False(customer.IsSuccess);
            Assert.NotEmpty(customer.Error!.messages);
            Assert.Equal(ErrorCode.BadRequest, customer.Error.errorCode);
        }
    }
}