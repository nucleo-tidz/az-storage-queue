namespace nucelotidz.storage.queue.Configuration
{
    public class StoargeConfiguration
    {
        public StoargeConfiguration()
        { }
        public StoargeConfiguration(string? connectionString, int retry)
        {
            ConnectionString = connectionString;
            Retry = retry;
        }

        public string? ConnectionString { get; init; }
        public int Retry { get; init; } = 2;
        public int BatchSize { get; init; } = 20;
    }
}
