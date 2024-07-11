using Core.Domain.Entities.Customers;

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

            var custormer = Custormer.Create(firstName, lastName, email, ssn);

            Assert.IsType<Custormer>(custormer);
            Assert.Equal(firstName, custormer.FirstName);
            Assert.Equal(lastName, custormer.LastName);
            Assert.Equal(email, custormer.Email);
            Assert.Equal(ssn, custormer.SSN.Value);
        }

        [Theory]
        [InlineData("", "Doe", "john.doe@bank.com", "416-27-7825")]
        [InlineData("John", null, "john.doe@bank.com", "416-27-7825")]
        [InlineData("John", "Doe", " ", "416-27-7825")]
        [InlineData("John", "Doe", "john.doe@bank.com", "")]
        public void Customer_Create_ShouldThrowAnException(string firstName, string lastName, string email, string ssn)
        {
            Assert.Throws<ArgumentNullException>(() => Custormer.Create(firstName, lastName, email, ssn));
        }
    }
}