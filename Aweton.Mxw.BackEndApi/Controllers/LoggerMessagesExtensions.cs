namespace Aweton.Mxw.BackEndApi.Controllers
{
  internal static partial class LoggerMessagesExtensions
  {
    [LoggerMessage(Level = LogLevel.Information, Message = "Producinsg {Number} of messages")]
    public static partial void InfoProducingForecast(this ILogger logger, int number);
    [LoggerMessage(Level = LogLevel.Debug, Message = "Request for Weather forecast received")]
    public static partial void DebugRequestReceived(this ILogger logger);
  }
}