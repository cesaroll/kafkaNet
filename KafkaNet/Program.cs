using Bogus;
using KafkaNet.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


Console.WriteLine("Hello, World!");
var host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((context, collection) =>
	{
		collection.AddHostedService<KafkaProducerService>();
		collection.AddHostedService<KafkaProducerBackgroundService>();
		collection.AddHostedService<KafkaConsumerService>();
	})
	.Build();

await host.RunAsync();
