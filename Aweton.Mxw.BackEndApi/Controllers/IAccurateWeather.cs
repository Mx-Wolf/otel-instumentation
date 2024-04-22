namespace Aweton.Mxw.BackEndApi.Controllers
{
  public interface IAccurateWeather
  {
    WeatherForecast Forecast(int index);
  }
}