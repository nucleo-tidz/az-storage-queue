using Azure.Core;
using Azure.Storage.Queues;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using nucelotidz.storage.queue.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nucelotidz.storage.queue.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        readonly ILogger<ConnectionFactory> _logger;
        readonly IOptions<StoargeConfiguration> _stoargeConfiguration;
        public ConnectionFactory(IOptions<StoargeConfiguration> stoargeConfiguration, ILogger<ConnectionFactory> logger)
        {
            ArgumentNullException.ThrowIfNull(stoargeConfiguration, nameof(StoargeConfiguration));
            stoargeConfiguration = _stoargeConfiguration;
            _logger = logger;
        }
        public QueueClient GetClient(string queueName)
        {

            QueueClientOptions queueClientOptions = new QueueClientOptions
            {
                Retry =
                {
                    MaxRetries=_stoargeConfiguration.Value.Retry,
                    Mode=RetryMode.Exponential,
                    Delay=TimeSpan.FromSeconds(5),

                }
            };
            queueClientOptions.MessageEncoding = QueueMessageEncoding.Base64;
            QueueClient client = new QueueClient(_stoargeConfiguration.Value.ConnectionString, queueName, queueClientOptions);
            return client;
        }
    }
}
