using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Banking.Contracts.Customer;

namespace Banking.Api.ModelMapping
{
    internal static class CustomerMapping
    {
        internal static CreateCustomerResponse Convert(this CreateCustomerCommandResponse command)
        {
            return new CreateCustomerResponse(command.Id, command.FirstName, command.LastName, command.Email, command.SSN);
        }
    }
}
