using Bogus;
using KafkaNet.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


Console.WriteLine("Hello, World!");
var host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((context, collection) =>
	{
		// collection.AddHostedService<KafkaBaseConsumerService>();
		// collection.AddHostedService<KafkaProducerService>();
		collection.AddHostedService<KafkaProducerA>();
		collection.AddHostedService<KafkaConsumerA>();
		collection.AddHostedService<KafkaConsumerB>();
		collection.AddHostedService<KafkaConsumerC>();
	})
	.Build();

await host.RunAsync();
