using Azure;
using Azure.Storage.Queues.Models;

namespace nucelotidz.storage.queue
{
    public interface IAdminQClient
    {
        Task<bool> CreateAsync(string queueName);
        Task<bool> DeleteAsync(string queueName);
        Task<Response> SetMetaDataAsync(IDictionary<string, string> metadata, string queueName);
    }
}
