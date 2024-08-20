using Banking.Application.Models;
using Banking.Application.Utils;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;

namespace Banking.Application.Features.Customers.Queries.GetCustomer
{
    public class GetCustomerQuery : IQuery<Result<GetCustomerDto, Error>>
    {
        public string CustomerId { get; init; } = string.Empty;
    }
}