using Aweton.Mxw.BackEndApi.Abstraction;
using Aweton.Mxw.BackEndApi.Controllers;
using Aweton.Mxw.BackEndApi.Services;
using Aweton.Mxw.Toolkit;
using DotPulsar;
using DotPulsar.Extensions;
using FluentValidation;
using Microsoft.Extensions.Internal;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
const string resourceName = "Aweton.Mx.Backend.Api";


var builder = WebApplication
    .CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(o =>
{
  o.Filters.Add<ResponseExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<IWeatherForecastService, WeatherForecastService>()
    .AddSingleton<IAccurateWeather, AccurateWeather>()
    .AddActivitySource(resourceName)
    .AddScoped<IValidator<WeatherForecastRequest>,WeatherForecastRequestValidator>()
    .AddSingleton<ISystemClock,PlatformSystemClock>()
    .AddSingleton((x)=>PulsarClient.Builder().Build()
    .NewProducer(Schema.String)
    .Topic("persistent://public/default/mytopic")
    .Create())
    ;

builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
  loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddOpenTelemetry()
  .ConfigureResource(b => b.AddService(resourceName))
  .WithTracing(b =>
  {
    b.AddConsoleExporter();
    b.AddAspNetCoreInstrumentation();
    b.AddSource(resourceName);
  })
  .WithMetrics(b => b.AddConsoleExporter());

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();