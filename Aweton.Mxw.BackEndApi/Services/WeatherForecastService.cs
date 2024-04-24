using Aweton.Mxw.BackEndApi.Abstraction;
using Aweton.Mxw.BackEndApi.Controllers;

namespace Aweton.Mxw.BackEndApi.Services
{
  internal class WeatherForecastService(ILogger<WeatherForecastService> logger, IAccurateWeather accurateWeather) : IWeatherForecastService
  {
    public async Task<IEnumerable<WeatherForecast>> GenerateForecast(int daysToForecast)
    {
      using var x = logger.BeginScope(("forecasting", daysToForecast));
      logger.InfoProducingForecast(daysToForecast);
      return await Task.WhenAll(Enumerable.Range(1, daysToForecast).Select(accurateWeather.Forecast));
    }
  }
}