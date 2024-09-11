using Banking.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.Redis;

namespace Banking.Api.IntegrationTests.TestBase
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime
        where TProgram : class
    {
        private readonly RedisContainer _redisContainer = new RedisBuilder()
            .WithImage("redis:latest")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services => 
            {
                var dbContextDescriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if(dbContextDescriptor is not null)     
                    services.Remove(dbContextDescriptor);


                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("ApplicationDbContextTest");
                });

                var redisContextDescriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(RedisCacheOptions));

                if(redisContextDescriptor is not null)
                    services.Remove(redisContextDescriptor);


                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = _redisContainer.GetConnectionString();
                });
            });

            builder.UseEnvironment("Development");
        }

        public Task InitializeAsync()
        {
            return _redisContainer.StartAsync();
        }
        public new Task DisposeAsync()
        {
            return _redisContainer.StopAsync();
        }
    }
}
