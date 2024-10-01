using Banking.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace Banking.Api.IntegrationTests.TestBase
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime
        where TProgram : class
    {
        private readonly RedisContainer _redisContainer = new RedisBuilder()
            .WithImage("redis:latest")
            .Build();

        private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("bankingtest")
            .WithUsername("test")
            .WithPassword("test")
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
                    options
                        .UseNpgsql(_postgresContainer.GetConnectionString())
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .UseSnakeCaseNamingConvention();
                });

                var redisContextDescriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(RedisCacheOptions));

                if(redisContextDescriptor is not null)
                    services.Remove(redisContextDescriptor);


                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = _redisContainer.GetConnectionString();
                });

                var outputCacheContextDescriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(OutputCacheOptions));

                if (outputCacheContextDescriptor is not null)
                    services.Remove(outputCacheContextDescriptor);

                services.AddStackExchangeRedisOutputCache(options =>
                {
                    var redis = _redisContainer.GetConnectionString();
                    options.Configuration = redis;
                });
            });

            builder.UseEnvironment("Development");
        }

        public async Task InitializeAsync()
        {
            await _redisContainer.StartAsync();
            await _postgresContainer.StartAsync();
        }
        public new async Task DisposeAsync()
        {
            await _redisContainer.StopAsync();
            await _postgresContainer.StopAsync();
        }
    }
}
