using Banking.Application.AppLogs;
using Banking.Application.Interfaces;
using Banking.Application.Mapping;
using Banking.Application.Utils;
using Banking.Domain.Entities.Customers;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using Microsoft.Extensions.Logging;

namespace Banking.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            ILogger<CreateCustomerCommandHandler> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<Result<Value, Error>> HandleAsync(CreateCustomerCommand command)
        {
            var customer = Customer.Create(
                firstName: command.FirstName,
                command.LastName,
                command.Email,
                command.SSN);

            if (!customer.IsSuccess) 
                return customer.Error!;

            await _customerRepository.AddAsync(customer.Value!);

            var value = customer.Value!.Convert();

            _logger.CustomerCreated(value);

            return value;
        }
    }
}