using Banking.Domain.Entities.Customers;
using Banking.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Persistence.Extensions
{
    internal static class CustormerExtension
    {
        internal static CustomerTable Convert(this Custormer customer)
        {
            return new()
            {
                CustomerId = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                SSN = customer.SSN.Value
            };
        }

        internal static Custormer Convert(this CustomerTable customer)
        {
            return Custormer.FromDB(
                customer.CustomerId,
                customer.FirstName,
                customer.LastName,
                customer.Email,
                customer.SSN);
        }
    }
}