using Banking.Domain.Entities.Customers;
using Banking.SharedKernel.Error;

namespace Domain.Tests
{
    public class SSNTests
    {
        [Fact]
        public void SSN_Instantiation_ShouldReturnObjectInstance()
        {
            var value = "416-27-7825";
            
            var ssn = SSN.Create(value);

            Assert.IsType<SSN>(ssn.Value);
            Assert.Equal(value, ssn.Value.Value);
        }

        [Theory]
        [InlineData("4162-27-7825")]
        [InlineData("")]
        public void SSN_Instantiation_ShouldThrowAnException(string value)
        {
            var ssn = SSN.Create(value);

            Assert.False(ssn.IsSuccess);
            Assert.IsType<Error>(ssn.Error);
            Assert.NotEmpty(ssn.Error.message);
        }
    }
}