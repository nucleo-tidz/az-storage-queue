using Azure.Storage.Queues;

namespace nucelotidz.storage.queue.Factory
{
    public interface IConnectionFactory
    {
        Azure.Storage.Queues.QueueClient GetClient(string queueName);
    }
}
