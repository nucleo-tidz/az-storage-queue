using Azure;
using Azure.Storage.Queues.Models;
using nucelotidz.storage.queue.Factory;
using nucelotidz.storage.queue.Serializers;

namespace nucelotidz.storage.queue
{
    public class Queue: IQueue
    {
        readonly IConnectionFactory _connectionFactory;
        readonly ISerializer _serializer;
        public Queue(IConnectionFactory connectionFactory, ISerializer serializer)
        {
            _connectionFactory = connectionFactory;
            _serializer = serializer;
        }
        public async Task<Response<SendReceipt>> Send<T>(string queueName, T dataObject)
        {
            string payload = _serializer.Serialize(dataObject);
            var queueClient = _connectionFactory.GetClient(queueName);
            if (!await queueClient.ExistsAsync())
            {
                throw new ApplicationException($"{queueName} doesnot exsit");
            }
            return await queueClient.SendMessageAsync(payload);
        }
        public async Task<List<T>> Consume<T>(string queueName)
        {
            List<T> result = new();
            var queueClient = _connectionFactory.GetClient(queueName);
            if (!await queueClient.ExistsAsync())
            {
                throw new ApplicationException($"{queueName} doesnot exsit");
            }
            Response<QueueMessage[]> responses = await queueClient.ReceiveMessagesAsync(20, TimeSpan.FromMinutes(5));
            foreach (var response in responses.Value)
            {
                result.Add(_serializer.Deserialize<T>(response.Body.ToString()));
                await queueClient.DeleteMessageAsync(response.MessageId, response.PopReceipt);
            }
            return result;
        }
    }
}
