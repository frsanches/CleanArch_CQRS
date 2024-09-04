using Banking.Api.ModelMapping;
using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Banking.Application.Features.Customers.Queries.GetCustomer;
using Banking.Application.Models;
using Banking.Application.Utils;
using Banking.Contracts.Customer;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICommandHandler<CreateCustomerCommand> _commandHandler;
        private readonly IQueryHandler<GetCustomerQuery, Result<GetCustomerDto, Error>> _queryHandler;

        public CustomersController(
            ICommandHandler<CreateCustomerCommand> commandHandler,
            IQueryHandler<GetCustomerQuery, Result<GetCustomerDto, Error>> queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetCustomerDto>> GetCustomerById(string customerId)
        {
            var customerQuery = new GetCustomerQuery { CustomerId = customerId };

            var result = await _queryHandler.HandleAsync(customerQuery);

            if (!result.IsSuccess)
                return StatusCode((int)result.Error!.errorCode, result.Error.messages);

            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateCustomerResponse>> AddCustomer(CreateCustomerRequest request)
        {
            var command = new CreateCustomerCommand(request.FirstName, request.LastName, request.Email, request.SSN);

            var result = await _commandHandler.HandleAsync(command);

            if (!result.IsSuccess)
                return StatusCode((int)result.Error!.errorCode, result.Error.messages);

            CreateCustomerResponse value = ((CreateCustomerCommandResponse)result.Value!).Convert();

            return CreatedAtAction(nameof(GetCustomerById), new { customerId = value.Id }, value);
        }
    }
}