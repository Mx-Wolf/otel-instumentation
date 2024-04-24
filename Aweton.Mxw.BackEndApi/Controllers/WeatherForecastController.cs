using Aweton.Mxw.BackEndApi.Abstraction;
using Aweton.Mxw.Toolkit;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable S2737

namespace Aweton.Mxw.BackEndApi.Controllers;

[ApiController]
[Route("weather-forecast")]
public class WeatherForecastController(
  ILogger<WeatherForecastController> logger,
  IWeatherForecastService weatherForecastService,
  IActivitySourceAccessor activitySourceAccessor,
  IValidator<WeatherForecastRequest> weatherForecastRequestValidator) : ControllerBase
{
  [HttpGet(Name = "weather-forecast")]
  public async Task<IEnumerable<WeatherForecast>> Get([FromQuery] WeatherForecastRequest request)
  {
    
    try
    {
      using var activity = activitySourceAccessor.ActivitySource.StartActivity();
      using var a = logger.BeginScope(new Dictionary<string, string> { ["demo-method"] = "Demo-Get", ["demo-scoped"] = "demo-true" });
      logger.DebugRequestReceived(request.Count);
      await weatherForecastRequestValidator.ValidateAndThrowAsync(request);
      return await weatherForecastService.GenerateForecast(request.Count);
   
    }
    catch when (Logged())
    {
      throw;
    }
  }

  private bool Logged()
  {
    logger.ControllerLevelScopeLogging();
    return false;
  }
}