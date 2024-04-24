using DotPulsar;

namespace Aweton.Mxw.BackEndApi.Controllers
{
  internal static partial class LoggerMessagesExtensions
  {
    [LoggerMessage(Level = LogLevel.Information, Message = "Producing forecast for the next {Number} days")]
    public static partial void InfoProducingForecast(this ILogger logger, int number);
    [LoggerMessage(Level = LogLevel.Debug, Message = "Request for Weather forecast received. Count: {count1}")]
    public static partial void DebugRequestReceived(this ILogger logger, int count1);

    [LoggerMessage(Level = LogLevel.Error, Message = "Central Error Logging Point")]
    public static partial void CentralErrorLogged(this ILogger logger, Exception exception);
    [LoggerMessage(Level = LogLevel.Error, Message = "Controller Level Exception Logging")]
    public static partial void ControllerLevelErrorLogging(this ILogger logger, Exception ex);
    [LoggerMessage(Level = LogLevel.Error, Message = "Controller Level scope logging without exception")]
    public static partial void ControllerLevelScopeLogging(this ILogger logger);
    [LoggerMessage(Level=LogLevel.Debug,  Message = "producing forecast for day# {offset}, id: {id}")]
    public static partial void AccurateWeatherActionLog(this ILogger logger, int offset, MessageId? id);
    [LoggerMessage(Level = LogLevel.Error, Message = "Request validation error")]
    public static partial void ValidationError(this ILogger logger, Exception exception);
    [LoggerMessage(Level=LogLevel.Error,Message="Cannot produce forecast for day:{day}")]
    public static partial void ErrorForecasting(this ILogger logger, int day, Exception exception);
  }
}