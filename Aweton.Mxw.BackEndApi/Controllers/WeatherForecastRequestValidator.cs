using FluentValidation;

namespace Aweton.Mxw.BackEndApi.Controllers
{
  
  internal class WeatherForecastRequestValidator : AbstractValidator<WeatherForecastRequest>
  {
    public WeatherForecastRequestValidator()
    {
      RuleFor(e => e.Count).GreaterThan(0).LessThanOrEqualTo(14);
    }
  }
}