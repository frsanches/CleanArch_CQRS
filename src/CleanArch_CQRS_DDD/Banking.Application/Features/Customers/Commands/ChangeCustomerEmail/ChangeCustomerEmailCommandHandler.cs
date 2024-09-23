using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Banking.Application.Interfaces;
using Banking.Application.Mapping;
using Banking.Application.Utils;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Banking.Application.Features.Customers.Commands.ChangeCustomerEmail
{
    public class ChangeCustomerEmailCommandHandler : ICommandHandler<ChangeCustomerEmailCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public ChangeCustomerEmailCommandHandler(
            ICustomerRepository customerRepository, 
            ILogger<CreateCustomerCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<Result<Value, Error>> HandleAsync(ChangeCustomerEmailCommand command)
        {
            if (!Guid.TryParse(command.CustomerId, out var customerId))
            {
                var error = new Error(ErrorCode.BadRequest, "The request is invalid.", [$"The value [{command.CustomerId}] is not a valid Customer Id"]);

                return error;
            }

            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
            {
                return new Error(ErrorCode.NotFound, "Ressource Not Found", [$"Customer Id [{command.CustomerId}] was not found."]);
            }

            var result = customer.ChangeEmail(command.Email);

            if (!result.IsSuccess) 
            {
                return result.Error!;
            }

            await _customerRepository.UpdateCustomerEmailAsync(customer);

            return customer.Convert();
        }
    }
}
