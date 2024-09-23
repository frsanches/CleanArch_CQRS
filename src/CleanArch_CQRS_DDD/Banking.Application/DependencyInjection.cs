using Banking.Application.Decorator;
using Banking.Application.Features.Customers.Commands.ChangeCustomerEmail;
using Banking.Application.Features.Customers.Commands.CreateCustomer;
using Banking.Application.Features.Customers.Queries.GetCustomer;
using Banking.Application.Models;
using Banking.Application.Utils;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateCustomerCommand>, CreateCustomerCommandHandler>();

            services.
                Decorate<ICommandHandler<CreateCustomerCommand>>((inner) 
                        => new ValidationDecorator<CreateCustomerCommand>(inner, new CreateCustomerCommandValidator()))
                .Decorate<ICommandHandler<CreateCustomerCommand>, IdempotencyDecorator<CreateCustomerCommand, CreateCustomerCommandResponse>>();

            services.AddTransient <ICommandHandler<ChangeCustomerEmailCommand>, ChangeCustomerEmailCommandHandler>();
            
            services.
                Decorate<ICommandHandler<ChangeCustomerEmailCommand>>((inner)
                        => new ValidationDecorator<ChangeCustomerEmailCommand>(inner, new ChangeCustomerEmailCommandValidator()));

            services.AddTransient<IQueryHandler<GetCustomerQuery, Result<GetCustomerDto, Error>>, GetCustomerQueryHandler>();

            return services;
        }
    }
}