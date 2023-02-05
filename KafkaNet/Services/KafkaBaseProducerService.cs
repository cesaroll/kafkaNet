using Bogus;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaNet.Services;

public class KafkaBaseProducerService : BackgroundService
{
	private readonly ILogger<KafkaProducerService> _logger;
	private readonly IProducer<Null, string> _producer;
	private readonly string _name;
	private readonly Random _random;
	
	public KafkaBaseProducerService(ILogger<KafkaProducerService> logger)
	{
		_logger = logger;
		var config = new ProducerConfig()
		{
			BootstrapServers = "localhost:9092"
		};
		_producer = new ProducerBuilder<Null, string>(config).Build();
		_name = new Faker().Name.FirstName();
		_random = new Random();
	}
	
	protected override Task ExecuteAsync(CancellationToken cancellationToken)
	{
		Task.Run(async () =>
		{
			var i = 0;
			while (!cancellationToken.IsCancellationRequested)
			{
				var value = $"{_name} producer. {i} - {DateTime.UtcNow.ToString("T")}";
				_logger.LogInformation($"######### -> {value}");
				await _producer.ProduceAsync(
					"demo", 
					new Message<Null, string> { Value = value },
					cancellationToken);
				await Task.Delay(_random.Next(1000, 5000), cancellationToken);
				i++;
			}

			_producer.Flush(TimeSpan.FromSeconds(10));
		}, cancellationToken);
		
		return Task.CompletedTask;
	}
}