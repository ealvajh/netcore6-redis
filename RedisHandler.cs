using StackExchange.Redis;

public class RedisHandler : IRedisHandler
{
    private readonly IConnectionMultiplexer _redis;
    private IDatabase _redisDb;

    // Inyectamos la interfaz de Redis
    public RedisHandler(IConnectionMultiplexer redis)
    {
        _redis = redis;

        // Se asigna la Base de datos de redis (0-15), por defecto es 0
        _redisDb = _redis.GetDatabase(db: 2);
    }


    // Regresamos el valor como string
    public string GetByKey(string key) => _redisDb.StringGet(key).ToString();

    // Registramos un nuevo dato key-value y se define un tiempo (segundos)
    public bool SetValue(string key, string value, DateTimeOffset timeToExpire)
    {
        var time = timeToExpire.DateTime.Subtract(DateTime.Now);

        return _redisDb.StringSet(key, value, time);
    } 

    // Se elimina un registro si existe especificando key
    public bool RemoveValue(string key)
    {
        var exist = _redisDb.KeyExists(key);

        if(exist)
            return _redisDb.KeyDelete(key);

        return false;
    }  
}