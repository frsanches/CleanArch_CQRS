using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Banking.Api.Idempotency
{
    public class IdempotentActionFilter<TResponse> : IAsyncActionFilter
    {
        private readonly ILogger<IdempotentActionFilter<TResponse>> _logger;
        private readonly IDistributedCache _distributedCache;
        public IdempotentActionFilter(
            ILogger<IdempotentActionFilter<TResponse>> logger,
            IDistributedCache distributedCache)
        {
            _logger = logger;
            _distributedCache = distributedCache;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool isPost = HttpMethods.IsPost(context.HttpContext.Request.Method);

            string? idemPotencyHeader = context.HttpContext.Request.Headers["X-IdempotencyKey"];

            if (isPost)
            {
                if (string.IsNullOrWhiteSpace(idemPotencyHeader))
                {
                    context.Result = new BadRequestObjectResult("X-Idempotency header was not set");
                    return;
                }

                var value = await _distributedCache.GetAsync(idemPotencyHeader!);

                if (value is not null)
                {
                    var cachedResult = JsonSerializer.Deserialize<TResponse>(Encoding.UTF8.GetString(value));

                    context.Result = new OkObjectResult(cachedResult);

                    _logger.LogInformation("Value retrieved from cache {@TResponse}", cachedResult);

                    return;
                }
            }

            var resultContext = await next();

            if (isPost)
            {
                var objResult = resultContext.Result as ObjectResult;

                if (objResult!.StatusCode == 200)
                {
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddHours(24));

                    await _distributedCache
                            .SetAsync(idemPotencyHeader!,
                            JsonSerializer.SerializeToUtf8Bytes(objResult.Value), options);
                }
            }
        }
    }
}