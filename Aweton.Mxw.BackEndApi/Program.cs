using Aweton.Mxw.BackEndApi.Controllers;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
const string ResourceName = "Aweton.Mx.Backend.Api";


var builder = WebApplication
    .CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<IWeatherForecastService, WeatherForecastService>()
    .AddSingleton<IAccurateWeather, AccurateWeather>()
    .AddActivitySource(ResourceName);
builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddOpenTelemetry()
  .ConfigureResource(b=>b.AddService(ResourceName))
  .WithTracing(b=>{
    b.AddConsoleExporter();
    b.AddAspNetCoreInstrumentation();
    b.AddSource(ResourceName);
  })
  .WithMetrics(b=>b.AddConsoleExporter());

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
