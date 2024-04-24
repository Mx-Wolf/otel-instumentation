namespace Aweton.Mxw.BackEndApi.Abstraction
{
    public interface IAccurateWeather
    {
        WeatherForecast Forecast(int index);
    }
}