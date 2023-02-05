using System.Text;
using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaNet.Services;

public class KafkaConsumerService : IHostedService
{
	private readonly ILogger<KafkaProducerService> _logger;
	private readonly ClusterClient _cluster;

	public KafkaConsumerService(ILogger<KafkaProducerService> logger)
	{
		_logger = logger;
		_cluster = new ClusterClient(new Configuration()
		{
			Seeds = "localhost:9092"
		}, new ConsoleLogger());
	}


	public Task StartAsync(CancellationToken cancellationToken)
	{
		_cluster.ConsumeFromLatest("demo");
		_cluster.MessageReceived += record =>
		{
			_logger.LogInformation(
				$" <<<<<<<<<<<<<<<< Consuming: {Encoding.UTF8.GetString(record.Value as byte[] ?? 
				                                                        Array.Empty<byte>())}");
		};
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		_cluster.Dispose();
		return Task.CompletedTask;
	}
}