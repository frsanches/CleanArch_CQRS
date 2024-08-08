using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Banking.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Application.Mapping
{
    internal static class CustomerExtension
    {
        internal static CreateCustomerResponse Convert(this Customer customer)
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