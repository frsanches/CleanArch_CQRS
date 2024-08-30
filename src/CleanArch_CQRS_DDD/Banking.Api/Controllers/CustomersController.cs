using Banking.Api.ApiLogs;
using Banking.Api.Idempotency;
using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Banking.Application.Features.Customers.Queries.GetCustomer;
using Banking.Application.Models;
using Banking.Application.Utils;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICommandHandler<CreateCustomerCommand, Result<CreateCustomerResponse, Error>> _commandHandler;
        private readonly IQueryHandler<GetCustomerQuery, Result<GetCustomerDto, Error>> _queryHandler;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
            ICommandHandler<CreateCustomerCommand, Result<CreateCustomerResponse, Error>> commandHandler,
            IQueryHandler<GetCustomerQuery, Result<GetCustomerDto, Error>> queryHandler,
            ILogger<CustomersController> logger)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
            _logger = logger;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [TypeFilter(typeof(IdempotentActionFilter<CreateCustomerResponse>))]
        public async Task<ActionResult<CreateCustomerResponse>> AddCustomer(CreateCustomerCommand command)
        {
            var result = await _commandHandler.HandleAsync(command);

            if (!result.IsSuccess)
                return StatusCode((int)result.Error!.errorCode, result.Error.messages);

            _logger.CustomerCreated(result.Value!);

            return Ok(result.Value);
        }
    }
}