using Shared.Register;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddRegister(builder.Configuration);
}


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
