using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Banking.Application.Utils;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Banking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateCustomerCommand, Result<CreateCustomerResponse, Error>>, CreateCustomerCommandHandler>();

            return services;
        }
    }
}