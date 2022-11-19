using Azure;
using Azure.Storage.Queues.Models;

namespace nucelotidz.storage.queue
{
    public interface IQueueClient
    {
        Task<Response<SendReceipt>> SendAsync<T>(string queueName, T dataObject);
        Task<List<T>> ConsumeAsync<T>(string queueName);
    }
}
