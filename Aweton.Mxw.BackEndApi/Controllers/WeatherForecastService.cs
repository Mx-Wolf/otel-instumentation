namespace Aweton.Mxw.BackEndApi.Controllers
{
  internal class WeatherForecastService(ILogger<WeatherForecastService> logger, IAccurateWeather accurateWeather) :IWeatherForecastService
  {
    private readonly ILogger<WeatherForecastService> logger = logger;
    private readonly IAccurateWeather accurateWeather = accurateWeather;

    public IEnumerable<WeatherForecast> GenerateForecast2()
    {
      const int daysToForecast = 5;
      logger.InfoProducingForecast(daysToForecast);
      return Enumerable.Range(1, daysToForecast).Select(accurateWeather.Forecast)
              .ToArray();
    }
  }
}