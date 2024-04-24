namespace Aweton.Mxw.BackEndApi.Abstraction
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> GenerateForecast(int daysToForecast);
    }
}