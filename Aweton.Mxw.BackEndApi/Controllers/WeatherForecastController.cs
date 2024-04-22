using Microsoft.AspNetCore.Mvc;

namespace Aweton.Mxw.BackEndApi.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController(
  ILogger<WeatherForecastController> logger, 
  IWeatherForecastService weatherForecastService, 
  IActivitySourceAccessor activitySourceAccessor) :ControllerBase
{
  private readonly ILogger<WeatherForecastController> logger = logger;
  private readonly IWeatherForecastService weatherForecastService = weatherForecastService;
  private readonly IActivitySourceAccessor activitySourceAccessor = activitySourceAccessor;

  [HttpGet(Name = "GetWeatherForecast")]
  public IEnumerable<WeatherForecast> Get()
  {
    using var activity = activitySourceAccessor.ActivitySource.StartActivity("Get weather forecast");
    using var a = logger.BeginScope(new Dictionary<string, string> { ["method"]="Get", ["scoped"]="true" });
    logger.DebugRequestReceived();
    return weatherForecastService.GenerateForecast2();
  }
}

