using Serilog;
using TrucksApi.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureSerilog();
builder.Services.ConfigureControllers();
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureSwagger();

builder.Host.ConfigureAutofacContainer();

var app = builder.Build();

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
