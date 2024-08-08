using Banking.Application.Utils;

namespace Banking.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : ICommand
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SSN { get; set; } = string.Empty;
    }
}