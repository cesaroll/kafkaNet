using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaNet.Services;

public class KafkaProducerService : IHostedService
{
	private readonly ILogger<KafkaProducerService> _logger;
	private readonly IProducer<Null, string> _producer;

	public KafkaProducerService(ILogger<KafkaProducerService> logger)
	{
		_logger = logger;
		var config = new ProducerConfig()
		{
			BootstrapServers = "localhost:9092"
		};
		_producer = new ProducerBuilder<Null, string>(config).Build();
	}
	
	public async Task StartAsync(CancellationToken cancellationToken)
	{
		for (var i =0; i < 20; i++)
		{
			var value = $"Hello World {i}";
			_logger.LogInformation(value);
			await _producer.ProduceAsync(
				"demo", 
				new Message<Null, string> { Value = value },
				cancellationToken);
			await Task.Delay(1000, cancellationToken);
		}

		_producer.Flush(TimeSpan.FromSeconds(10));
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		_producer.Dispose();
		return Task.CompletedTask;
	}
}