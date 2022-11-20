using nucelotidz.storage.queue;
using nucleotidz.queue.example;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddQueueClient(hostContext.Configuration.GetSection("StorageConfiguration"));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
