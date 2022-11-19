namespace nucelotidz.storage.queue.Configuration
{
    public class StorageConfiguration
    {
        public StorageConfiguration()
        { }
        public StorageConfiguration(string? connectionString, int retry)
        {
            ConnectionString = connectionString;
            Retry = retry;
        }

        public string? ConnectionString { get; init; }
        public int Retry { get; init; } = 2;
        public int BatchSize { get; init; } = 20;
    }
}
