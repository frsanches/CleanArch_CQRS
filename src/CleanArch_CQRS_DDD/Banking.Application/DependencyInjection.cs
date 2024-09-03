using Banking.Application.Decorator;
using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Banking.Application.Features.Customers.Queries.GetCustomer;
using Banking.Application.Interfaces;
using Banking.Application.Models;
using Banking.Application.Utils;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Banking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateCustomerCommand>>(x =>
                new IdempotencyDecorator<CreateCustomerCommand, CreateCustomerResponse>(
                    new CreateCustomerCommandHandler(
                        x.GetService<ICustomerRepository>()!,
                        x.GetService<ILogger<CreateCustomerCommandHandler>>()!),
                    x.GetService<IDistributedCache>()!,
                    x.GetService<IHttpContextAccessor>()!
                   )
                );

            services.AddTransient<IQueryHandler<GetCustomerQuery, Result<GetCustomerDto, Error>>, GetCustomerQueryHandler>();

            return services;
        }
    }
}