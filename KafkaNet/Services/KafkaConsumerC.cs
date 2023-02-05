using Microsoft.Extensions.Logging;

namespace KafkaNet.Services;

public class KafkaConsumerC : KafkaBaseConsumerService
{
	public KafkaConsumerC(ILogger<KafkaProducerService> logger) : base(logger)
	{
	}
}