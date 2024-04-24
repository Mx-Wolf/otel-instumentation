using System.Diagnostics;
using Aweton.Mxw.BackEndApi.Abstraction;
using Aweton.Mxw.BackEndApi.Controllers;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using Microsoft.Extensions.Internal;
using Serilog.Configuration;

namespace Aweton.Mxw.BackEndApi.Services
{
  internal class AccurateWeather(ILogger<AccurateWeather> logger, ISystemClock systemClock, IProducer<string> auditProducer) : IAccurateWeather
  {
    private static readonly string[] Summaries =
    [
      "Freezing",
      "Bracing",
      "Chilly",
      "Cool",
      "Mild",
      "Warm",
      "Balmy",
      "Hot",
      "Sweltering",
      "Scorching"
    ];

    public async Task<WeatherForecast> Forecast(int index)
    {
      try
      {
        return await GetForecastInternal(index);
      }
      catch (Exception ex) when (Logged(index, ex))
      {
        throw;
      }
    }

    private bool Logged(int index, Exception exception)
    {
      logger.ErrorForecasting(index, exception);
      return true;
    }

    private async Task<WeatherForecast> GetForecastInternal(int index)
    {
      var theDate = DateOnly.FromDateTime(systemClock.UtcNow.DateTime.AddDays(index));
      using var cts = new CancellationTokenSource();
      cts.CancelAfter(TimeSpan.FromSeconds(2));
      //var id = await auditProducer.Send(Prepare(), theDate.ToString("o"), cts.Token);      
      logger.AccurateWeatherActionLog(index, null);
      await Task.CompletedTask;
      if (index > 7)
      {
        throw new ArgumentException("too many");
      }

      return new WeatherForecast
      {
        Date = theDate,
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      };
    }

    private MessageMetadata Prepare()
    {
      var (traceparent, tracestate) = (Activity.Current?.Id,  Activity.Current?.TraceStateString);
      return new MessageMetadata()
      .AddProperty("traceparent", traceparent)
      .AddProperty("tracestate",tracestate);
    }
  }
}