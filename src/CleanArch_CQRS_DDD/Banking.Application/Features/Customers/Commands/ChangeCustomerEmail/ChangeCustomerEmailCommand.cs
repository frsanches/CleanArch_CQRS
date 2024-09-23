
using Banking.Application.Utils;

namespace Banking.Application.Features.Customers.Commands.ChangeCustomerEmail
{
    public class ChangeCustomerEmailCommand : ICommand
    {
        public string CustomerId { get; init; }
        public string Email { get; init; }

        public ChangeCustomerEmailCommand(string customerId, string email)
        {
            CustomerId = customerId;
            Email = email;
        }
    }
}
