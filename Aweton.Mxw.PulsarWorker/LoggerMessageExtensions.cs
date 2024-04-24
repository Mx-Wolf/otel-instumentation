using Microsoft.Extensions.Logging;

namespace Aweton.Mxw.PulsarWorker
{
  internal static partial class LoggerMessageExtensions
  {
    [LoggerMessage(Level = LogLevel.Information, Message = "Received: {received}")]
    public static partial void InformationMessageReceived(this ILogger logger, string received);
    [LoggerMessage(Level=LogLevel.Information, Message = "starting worker")]
    public static partial void DebugBeforeLoop(this ILogger logger);
    [LoggerMessage(Level = LogLevel.Information, Message = "exiting the loop")]
    public static partial void DebugExitingLoop(this ILogger logger);
  }
}