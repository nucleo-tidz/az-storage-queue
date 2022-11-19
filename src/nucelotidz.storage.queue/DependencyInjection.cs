using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using nucelotidz.storage.queue.Configuration;
using nucelotidz.storage.queue.Factory;

namespace nucelotidz.storage.queue
{
    public static class DependencyInjection
    {
        public static void Add(this IServiceCollection services, IConfigurationSection configuartionSection)
        {
            services.Configure<StoargeConfiguration>(configuartionSection);
            services.AddTransient<IConnectionFactory, ConnectionFactory>();
        }
    }
}
