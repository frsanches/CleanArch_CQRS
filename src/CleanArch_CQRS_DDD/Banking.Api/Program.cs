using Banking.Api.Configuration;
using OpenTelemetry.Logs;
using Shared.Register;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services
        .AddRegister(builder.Configuration)
        .AddTelemetry();

    builder.Logging.AddOpenTelemetry(logging =>
    {
        logging.IncludeFormattedMessage = true;
        logging.IncludeScopes = true;

        logging.AddOtlpExporter();
    });

    builder.Services.AddProblemDetails(ops =>
        ops.CustomizeProblemDetails = (ctx) =>
        {
            if (ctx.ProblemDetails.Status == 500)
                ctx.ProblemDetails.Detail = "An error occuered in our API";
        }
    );

    builder.Services.AddStackExchangeRedisCache(options =>
    {
        var redis = builder.Configuration.GetConnectionString("Redis");

        options.Configuration = redis;
    });


    builder.Services.AddOutputCache(options => options.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(2));
    
    builder.Services.AddStackExchangeRedisOutputCache(options =>
    {
        var redis = builder.Configuration.GetConnectionString("Redis");
        options.Configuration = redis;
        options.InstanceName = "banking.redis";
    });

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseOutputCache();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.Run();

public partial class Program { }