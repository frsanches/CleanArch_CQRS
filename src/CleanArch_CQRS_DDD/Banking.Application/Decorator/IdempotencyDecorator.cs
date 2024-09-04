using Banking.Application.Utils;
using Banking.SharedKernel.Error;
using Banking.SharedKernel.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Banking.Application.Decorator
{
    public sealed class IdempotencyDecorator<TCommand, TResponse> : ICommandHandler<TCommand>
        where TCommand : ICommand
        where TResponse : Value
    {
        private readonly ICommandHandler<TCommand> _handler;
        private readonly IDistributedCache _distributedCache;
        private readonly IHttpContextAccessor _contextAccessor;
        public IdempotencyDecorator(
            ICommandHandler<TCommand> handler,
            IDistributedCache distributedCache,
            IHttpContextAccessor contextAccessor)
        {
            _handler = handler;
            _distributedCache = distributedCache;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result<Value, Error>> HandleAsync(TCommand command)
        {
            string? idemPotencyHeader = _contextAccessor.HttpContext.Request.Headers["X-IdempotencyKey"];

            if (string.IsNullOrWhiteSpace(idemPotencyHeader))
            {
                return new Error(ErrorCode.BadRequest, ["X-Idempotency header was not set"]);
            }

            var value = await _distributedCache.GetAsync(idemPotencyHeader!);

            if (value is not null)
            {
                var cachedResult = JsonSerializer.Deserialize<TResponse>(Encoding.UTF8.GetString(value));

                return cachedResult!;
            }

            var result = await _handler.HandleAsync(command);

            if (result.IsSuccess)
            {
                var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddHours(24));

                await _distributedCache
                        .SetAsync(idemPotencyHeader!,
                        JsonSerializer.SerializeToUtf8Bytes((TResponse)result.Value!), options);
            }

            return result;
        }
    }
}