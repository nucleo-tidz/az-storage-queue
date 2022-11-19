using Azure.Core;
using Azure.Storage.Queues;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using nucelotidz.storage.queue.Configuration;

namespace nucelotidz.storage.queue.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IOptions<StoargeConfiguration> _stoargeConfiguration;
        public ConnectionFactory(IOptions<StoargeConfiguration> stoargeConfiguration)
        {
            ArgumentNullException.ThrowIfNull(stoargeConfiguration, nameof(StoargeConfiguration));
            _stoargeConfiguration = stoargeConfiguration;
        }
        public Azure.Storage.Queues.QueueClient GetClient(string queueName)
        {

            QueueClientOptions queueClientOptions = new QueueClientOptions
            {
                Retry =
                {
                    MaxRetries=_stoargeConfiguration.Value.Retry,
                    Mode=RetryMode.Exponential,
                    Delay=TimeSpan.FromSeconds(5),

                },
                MessageEncoding = QueueMessageEncoding.Base64
            };
            var client = new QueueClient(_stoargeConfiguration.Value.ConnectionString, queueName, queueClientOptions);
            return client;
        }
    }
}
