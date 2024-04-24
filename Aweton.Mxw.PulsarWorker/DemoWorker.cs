using System.Diagnostics;
using Aweton.Mxw.Toolkit;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aweton.Mxw.PulsarWorker;

internal class DemoWorker(IConsumer<string> consumer, IServiceProvider serviceProvider, IActivitySourceAccessor activitySourceAccessor) : BackgroundService
{

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var message in consumer.Messages(stoppingToken))
    {
      var result = await HandleNextMessage(message, stoppingToken);
      if (result)
      {
        await consumer.Acknowledge(message, stoppingToken);
      }
    }
  }

  private async Task<bool> HandleNextMessage(IMessage<string> message, CancellationToken stoppingToken)
  {
    using var sp = serviceProvider.CreateScope();
    using var activity = activitySourceAccessor.ActivitySource.StartActivity(
      kind: ActivityKind.Server,
      parentContext: ParentContext(message));
   
    return await sp.ServiceProvider.GetRequiredService<IMessageHandler>().Handle(message, stoppingToken);
  }

  private static ActivityContext ParentContext(IMessage? message)
  {
    var (traceparent, tracestate) = (message?.Properties["traceparent"], message?.Properties["tracestate"]);
    return traceparent == null ? default : ActivityContext.Parse(traceparent, tracestate);
  }
}