using Azure;
using Azure.Storage.Queues.Models;

namespace nucelotidz.storage.queue
{
    public interface IQueue
    {
        Task<Response<SendReceipt>> Send<T>(string queueName, T dataObject);
        Task<List<T>> Consume<T>(string queueName);

    }
}
