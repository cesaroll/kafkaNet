using System.Text;
using Bogus;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaNet.Services;

public class KafkaBaseConsumerService : BackgroundService
{
	private readonly ILogger<KafkaProducerService> _logger;
	private readonly ConsumerConfig _consumerConfig;
	private readonly string _name;

	public KafkaBaseConsumerService(ILogger<KafkaProducerService> logger)
	{
		_logger = logger;
		_consumerConfig = new ConsumerConfig
		{
			BootstrapServers = "localhost:9092",
			GroupId = "consumer-group-1",
			AutoOffsetReset = AutoOffsetReset.Earliest
		};
		_name = new Faker().Name.FirstName();
	}


	protected override Task ExecuteAsync(CancellationToken cancellationToken)
	{
		Task.Run( () =>
		{
			using(var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build())
			{
				consumer.Subscribe("demo");

				while(!cancellationToken.IsCancellationRequested)
				{
					var consumeResult = consumer.Consume(cancellationToken);
					_logger.LogInformation(
						$" <<<<<<<<<<<<<<<< {_name} Consumer: {consumeResult.Message.Value}");
				}
				consumer.Close();
			}
		}, cancellationToken);

		return Task.CompletedTask;
	}
}