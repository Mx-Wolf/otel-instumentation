using Aweton.Mxw.BackEndApi.Abstraction;
using Aweton.Mxw.BackEndApi.Controllers;

namespace Aweton.Mxw.BackEndApi.Services
{
  internal class AccurateWeather(ILogger<AccurateWeather> logger) : IAccurateWeather
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

    public WeatherForecast Forecast(int index)
    {
      logger.AccurateWeatherActionLog(index);
      if (index > 7)
      {
        throw new ArgumentException("too many");
      }
      return new WeatherForecast
      {
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
      };
    }
  }
}