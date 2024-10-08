﻿using Banking.Application;
using Banking.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Register
{
    public static class GlobalDepencyInjection
    {
        public static IServiceCollection AddRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPersistence(configuration)
                .AddApplication();

            return services;
        }
    }
}
