using DotPulsar.Abstractions;

namespace Aweton.Mxw.PulsarWorker
{
  public interface IMessageHandler
  {
    Task<bool> Handle(IMessage<string> message, CancellationToken stoppingToken);
  }
}