using Banking.Application.Utils;

namespace Banking.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : ICommand
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }
        public string SSN { get; init; }

        public CreateCustomerCommand(string firstName, string lastName, string email, string ssn)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            SSN = ssn;
        }
    }
}