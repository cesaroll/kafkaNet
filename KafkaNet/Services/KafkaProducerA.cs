using Microsoft.Extensions.Logging;

namespace KafkaNet.Services;

public class KafkaProducerA : KafkaBaseProducerService
{
	public KafkaProducerA(ILogger<KafkaProducerService> logger) : base(logger)
	{
	}
}