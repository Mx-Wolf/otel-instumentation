using Aweton.Mxw.BackEndApi.Abstraction;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

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
    catch (Exception e) when (Logged(e))
    {
      e.Data["controller"] = "handled";
      throw;
    }
  }

  private bool Logged(Exception exception)
  {
    logger.ControllerLevelErrorLogging(exception);
    return true;
  }
}