using Azure;
using Azure.Storage.Queues.Models;

namespace nucelotidz.storage.queue
{
    public interface IQClient
    {
        Task<Response<SendReceipt>> SendAsync<T>(string queueName, T dataObject, TimeSpan ttl);
        Task<List<T>> ConsumeAsync<T>(string queueName);
        Task<List<T>> PeekAsync<T>(string queueName);
        Task<QueueProperties> GetPropertiesAsync(string queueName);
        Task<Response> PurgeAsync(string queueName);
    }
}
