using Banking.Application.Interfaces;
using Banking.Application.Mapping;
using Banking.Application.Utils;
using Banking.Domain.Entities.Customers;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;

namespace Banking.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Result<CreateCustomerResponse, Error>>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Result<CreateCustomerResponse, Error> Execute(CreateCustomerCommand command)
        {
            // TODO : command validation using fluentvalidation

            var customer = Customer.Create(
                firstName: command.FirstName,
                command.LastName,
                command.Email,
                command.SSN);

            _customerRepository.AddAsync(customer.Value!);

            return customer.Value!.Convert();
        }
    }
}