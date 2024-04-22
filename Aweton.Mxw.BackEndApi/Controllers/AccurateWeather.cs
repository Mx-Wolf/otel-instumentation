namespace Aweton.Mxw.BackEndApi.Controllers
{
  internal class AccurateWeather:IAccurateWeather
  {
    private static readonly string[] Summaries =
    [
          "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public static WeatherForecast ForecastDay(int index)
    {
      return new AccurateWeather().Forecast(index);
    }

    public WeatherForecast Forecast(int index)
    {
      return new WeatherForecast
      {
        Date=DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC=Random.Shared.Next(-20, 55),
        Summary=Summaries[Random.Shared.Next(Summaries.Length)]
      };
    }
  }
}