namespace Aweton.Mxw.BackEndApi.Abstraction
{
    public interface IAccurateWeather
    {
        Task<WeatherForecast> Forecast(int index);
    }
}