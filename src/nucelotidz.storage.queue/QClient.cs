using Azure;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Options;
using nucelotidz.storage.queue.Configuration;
using nucelotidz.storage.queue.Factory;
using nucelotidz.storage.queue.Serializers;

namespace nucelotidz.storage.queue
{
    public class QClient : IQClient
    {
        private readonly IOptions<StorageConfiguration> _storageConfiguration;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ISerializer _serializer;
        public QClient(IConnectionFactory connectionFactory, ISerializer serializer, IOptions<StorageConfiguration> storageConfiguration)
        {
            _connectionFactory = connectionFactory;
            _serializer = serializer;
            _storageConfiguration = storageConfiguration;

        }
        public async Task<Response<SendReceipt>> SendAsync<T>(string queueName, T dataObject, TimeSpan ttl)
        {
            string payload = _serializer.Serialize(dataObject);
            var queueClient = _connectionFactory.GetClient(queueName);
            if (!await queueClient.ExistsAsync())
            {
                throw new ApplicationException($"{queueName} doesnot exsit");
            }
            return await queueClient.SendMessageAsync(payload, timeToLive: ttl);
        }
        public async Task<List<T>> ConsumeAsync<T>(string queueName)
        {
            List<T> result = new();
            var queueClient = _connectionFactory.GetClient(queueName);
            if (!await queueClient.ExistsAsync())
            {
                throw new ApplicationException($"{queueName} doesnot exsit");
            }

            Response<QueueMessage[]> responses = await queueClient.ReceiveMessagesAsync(_storageConfiguration.Value.BatchSize, TimeSpan.FromMinutes(10));
            foreach (QueueMessage response in responses.Value)
            {
                result.Add(_serializer.Deserialize<T>(response.Body.ToString()));
                await queueClient.DeleteMessageAsync(response.MessageId, response.PopReceipt);
            }
            return result;
        }
        public async Task<List<T>> PeekAsync<T>(string queueName)
        {
            List<T> result = new();
            var queueClient = _connectionFactory.GetClient(queueName);
            if (!await queueClient.ExistsAsync())
            {
                throw new ApplicationException($"{queueName} doesnot exsit");
            }          
            Response<PeekedMessage[]> responses = await queueClient.PeekMessagesAsync(_storageConfiguration.Value.BatchSize);
            foreach (PeekedMessage response in responses.Value)
            {
                result.Add(_serializer.Deserialize<T>(response.Body.ToString()));
            }
            return result;
        }
        public async Task<QueueProperties> GetPropertiesAsync(string queueName)
        {
            var queueClient = _connectionFactory.GetClient(queueName);
            if (!await queueClient.ExistsAsync())
            {
                throw new ApplicationException($"{queueName} doesnot exsit");
            }

            Response<QueueProperties> responses = await queueClient.GetPropertiesAsync();
            return responses.Value;
        }
        public async Task<Response> PurgeAsync(string queueName)
        {
            var queueClient = _connectionFactory.GetClient(queueName);
            if (!await queueClient.ExistsAsync())
            {
                throw new ApplicationException($"{queueName} doesnot exsit");
            }
            Response response = await queueClient.ClearMessagesAsync();
            return response;
        }
    }
}
