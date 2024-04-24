using System.Diagnostics;
using Aweton.Mxw.BackEndApi.Abstraction;
using Aweton.Mxw.BackEndApi.Controllers;
using DotPulsar;
using DotPulsar.Abstractions;
using Microsoft.Extensions.Internal;

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
      var theDate = DateOnly.FromDateTime(systemClock.UtcNow.DateTime.AddDays(index));
      var id = await auditProducer.Send(Prepare(), theDate.ToString("o"));      
      logger.AccurateWeatherActionLog(index, id);
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

internal static class MessageMetadataExtensions{
  public static MessageMetadata AddProperty(this MessageMetadata self, string name, string? value){
    if(!string.IsNullOrWhiteSpace(value)){
      self[name] = value;
    }
    return self;
  }
}