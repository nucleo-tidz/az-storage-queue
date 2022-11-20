using Azure;
using Azure.Storage.Queues;
using nucelotidz.storage.queue.Factory;

namespace nucelotidz.storage.queue
{
    internal class AdminQClient : IAdminQClient
    {
        private readonly IConnectionFactory _connectionFactory;

        public AdminQClient(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<bool> CreateAsync(string queueName)
        {
            var queueClient = _connectionFactory.GetClient(queueName);
            await queueClient.CreateIfNotExistsAsync();
            return await queueClient.ExistsAsync();
        }
        public async Task<bool> DeleteAsync(string queueName)
        {
            var queueClient = _connectionFactory.GetClient(queueName);
            await queueClient.DeleteIfExistsAsync();
            return !await queueClient.ExistsAsync();
        }
        public async Task<Response> SetMetaDataAsync(IDictionary<string, string> metadata, string queueName)
        {
            var queueClient = _connectionFactory.GetClient(queueName);
            if (!await queueClient.ExistsAsync())
            {
                throw new ApplicationException($"{queueName} doesnot exsit");
            }           
            return await queueClient.SetMetadataAsync(metadata);
        }
    }
}
