using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using nucelotidz.storage.queue.Configuration;
using nucelotidz.storage.queue.Factory;
using nucelotidz.storage.queue.Serializers;

namespace nucelotidz.storage.queue
{
    public static class DependencyInjection
    {
        public static void AddQueueClient(this IServiceCollection services, IConfigurationSection configuartionSection)
        {
            services.Configure<StoargeConfiguration>(configuartionSection);
            services.AddTransient<ISerializer, Json>();
            services.AddTransient<IConnectionFactory, ConnectionFactory>();
            services.AddTransient<IQueueClient, QueueClient>();
        }
    }
}
