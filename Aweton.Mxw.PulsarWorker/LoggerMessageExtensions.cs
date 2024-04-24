using Microsoft.Extensions.Logging;

namespace Aweton.Mxw.PulsarWorker
{
  internal static partial class LoggerMessageExtensions
  {
    [LoggerMessage(Level = LogLevel.Information, Message = "Received: {received}")]
    public static partial void InformationMessageReceived(this ILogger logger, string received);
  }
}