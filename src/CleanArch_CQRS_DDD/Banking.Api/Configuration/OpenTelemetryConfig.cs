using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Banking.Api.Configuration
{
    public static class OpenTelemetryConfig
    {
        public static IServiceCollection AddTelemetry(this IServiceCollection services)
        {
            services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService("Banking.Api"))
                .WithMetrics(metrics =>
                {
                    metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

                    metrics.AddOtlpExporter();
                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation();

                    tracing.AddOtlpExporter();

                });

            return services;
        }
    }
}