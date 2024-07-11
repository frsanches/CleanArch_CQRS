using Core.Domain.Entities.Customers;

namespace Domain.Tests
{
    public class SSNTests
    {
        [Fact]
        public void SSN_Instantiation_ShouldReturnObjectInstance()
        {
            var value = "416-27-7825";

            var ssn = new SSN(value);

            Assert.IsType<SSN>(ssn);
            Assert.Equal(value, ssn.Value);
        }

        [Theory]
        [InlineData("4162-27-7825")]
        [InlineData("")]
        public void SSN_Instantiation_ShouldThrowAnException(string value)
        {
            Assert.Throws<ArgumentException>(() => new SSN(value));
        }
    }
}