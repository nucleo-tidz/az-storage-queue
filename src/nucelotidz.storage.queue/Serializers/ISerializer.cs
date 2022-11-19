namespace nucelotidz.storage.queue.Serializers
{
    public interface ISerializer
    {
        string Serialize<T>(T dataObject);
        T Deserialize<T>(string data);
    }
}
