using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Microsoft.Extensions.Logging;

namespace Banking.Application.AppLogs
{
    internal static partial class AppLogsGenerator
    {
        [LoggerMessage(LogLevel.Information, "Customer created {customer}")]
        public static partial void CustomerCreated(this ILogger logger, [LogProperties] CreateCustomerResponse customer);
    }
}