using Microsoft.AspNetCore.Mvc;

namespace Aweton.Mxw.BackEndApi.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController(
  ILogger<WeatherForecastController> logger, 
  IWeatherForecastService weatherForecastService, 
  IActivitySourceAccessor activitySourceAccessor) :ControllerBase
{
    [HttpGet(Name = "GetWeatherForecast")]
  public IEnumerable<WeatherForecast> Get()
  {
    using var activity = activitySourceAccessor.ActivitySource.StartActivity();
    using var a = logger.BeginScope(new Dictionary<string, string> { ["demo-method"]="Demo-Get", ["demo-scoped"]="demo-true" });
    logger.DebugRequestReceived();
    return weatherForecastService.GenerateForecast2();
  }
}

