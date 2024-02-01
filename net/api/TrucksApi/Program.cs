using Serilog;
using TrucksApi.Configuration;
using TrucksApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog();
builder.Services.ConfigureApi();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwaggerApplication();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
