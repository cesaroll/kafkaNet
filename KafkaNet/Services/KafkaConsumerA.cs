using Microsoft.Extensions.Logging;

namespace KafkaNet.Services;

public class KafkaConsumerA : KafkaBaseConsumerService
{
	public KafkaConsumerA(ILogger<KafkaProducerService> logger) : base(logger)
	{
	}
}