using Microsoft.Extensions.Logging;

namespace KafkaNet.Services;

public class KafkaConsumerB : KafkaBaseConsumerService
{
	public KafkaConsumerB(ILogger<KafkaProducerService> logger) : base(logger)
	{
	}
}