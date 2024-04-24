using System.Runtime.CompilerServices;
using Aweton.Mxw.PulsarWorker;
using Aweton.Mxw.Toolkit;
using DotPulsar;
using DotPulsar.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

const string sourceName = "Aweton.Mxw.PulsarWorker";
const string subscriptionName = "Aweton.Mxw.DemoSubscription";

var builder = Host.CreateApplicationBuilder();
builder.Services
  .AddHostedService<DemoWorker>()
  .AddActivitySource(sourceName)
  .AddTransient<IMessageHandler, MessageHandler>()
  .AddSingleton((x) => PulsarClient.Builder().Build()
    .NewConsumer(Schema.String)
    .SubscriptionName(subscriptionName)
    .Topic("persistent://public/default/mytopic")
    .Create());

builder.Services.AddOpenTelemetry()
  .ConfigureResource((b) =>
  {
    b.AddService(sourceName);
  })
  .WithTracing(b =>
  {
    b.AddConsoleExporter();
    b.AddSource(sourceName);
  });

await builder.Build().RunAsync();