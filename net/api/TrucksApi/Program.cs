using Asp.Versioning;
using TrucksApi.Configuration;
using TrucksApi.Configuration.ApiVersioning;
using TrucksManager.Trucks.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers(options => options.UseRoutePrefix("api/v{version:apiVersion}"))
    .AddApplicationPart(typeof(AssemblyMarker).Assembly);

builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ConfigureSwaggerApplication();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
