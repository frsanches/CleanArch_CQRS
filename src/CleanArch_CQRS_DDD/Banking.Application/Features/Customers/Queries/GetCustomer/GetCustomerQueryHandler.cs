using Banking.Application.Interfaces;
using Banking.Application.Mapping;
using Banking.Application.Models;
using Banking.Application.Utils;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;

namespace Banking.Application.Features.Customers.Queries.GetCustomer
{
    public class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, Result<GetCustomerDto, Error>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Result<GetCustomerDto, Error>> HandleAsync(GetCustomerQuery query)
        {
            if (!Guid.TryParse(query.CustomerId, out var customerId))
            {
                var error = new Error(ErrorCode.BadRequest, [$"The value [{query.CustomerId}] is not a valid Customer Id"]);

                return error;
            }

            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                return new Error(ErrorCode.NotFound, [$"Customer Id [{query.CustomerId}] was not found."]);
            }

            return customer!.ConvertToDto();
        }
    }
}