public interface IRedisHandler
{
    string GetByKey(string key);

    bool SetValue(string key, string value, DateTimeOffset timeTo);

    bool RemoveValue(string key);
}