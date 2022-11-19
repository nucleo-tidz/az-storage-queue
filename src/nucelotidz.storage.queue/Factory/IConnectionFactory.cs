using Azure.Storage.Queues;

namespace nucelotidz.storage.queue.Factory
{
    public interface IConnectionFactory
    {
        QueueClient GetClient(string queueName);
    }
}
