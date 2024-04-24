using DotPulsar.Abstractions;
using Microsoft.Extensions.Logging;

namespace Aweton.Mxw.PulsarWorker
{
  internal class MessageHandler(ILogger<MessageHandler> logger) : IMessageHandler
  {
    public async Task<bool> Handle(IMessage<string> message, CancellationToken stoppingToken)
    {
      if (stoppingToken.IsCancellationRequested)
      {
        return await Task.FromResult(false);
      }
      logger.InformationMessageReceived(message.Value());
      return await Task.FromResult(true);
    }
  }
}