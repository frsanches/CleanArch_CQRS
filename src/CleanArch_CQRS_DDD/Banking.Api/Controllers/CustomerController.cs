using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Banking.Application.Utils;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICommandHandler<CreateCustomerCommand, Result<CreateCustomerResponse, Error>> _commandHandler;

        public CustomerController(ICommandHandler<CreateCustomerCommand, Result<CreateCustomerResponse, Error>> commandHandler)
        {
            _commandHandler = commandHandler;
        }

        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(string customerId)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateCustomerResponse>> AddCustomer(CreateCustomerCommand command)
        {
            var result = await _commandHandler.HandleAsync(command);

            if (!result.IsSuccess)
                return StatusCode((int)result.Error!.errorCode, result.Error.messages);

            return Ok(result.Value);
        }
    }
}
