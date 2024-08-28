using Banking.Application.Features.Customers.Commands.CreateCustomer;

namespace Banking.Api.ApiLogs
{
    static partial class LogsGenerator
    {
        [LoggerMessage(LogLevel.Information, "Customer created {customer}")]
        public static partial void CustomerCreated(this ILogger logger, [LogProperties] CreateCustomerResponse customer);
    }
}