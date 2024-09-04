using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Banking.Application.Models;
using Banking.Domain.Entities.Customers;

namespace Banking.Application.Mapping
{
    internal static class CustomerExtension
    {
        internal static CreateCustomerCommandResponse Convert(this Customer customer)
        {
            return new()
            {
                Id = customer.Id.ToString(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                SSN = customer.SSN.Value
            };
        }

        internal static GetCustomerDto ConvertToDto(this Customer customer)
        {
            return new()
            {
                Id = customer.Id.ToString(),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                SSN = customer.SSN.Value
            };
        }
    }
}