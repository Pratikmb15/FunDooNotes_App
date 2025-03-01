using StackExchange.Redis;
using System;
using System.Text.Json;

namespace RepositoryLayer.Services
{
    public class RedisCacheService
    {
        private readonly IDatabase _cache;
        private static ConnectionMultiplexer _redis;

        public RedisCacheService(string connectionString)
        {
            if (_redis == null)
            {
                _redis = ConnectionMultiplexer.Connect(connectionString);
            }
            _cache = _redis.GetDatabase();
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            string jsonData = JsonSerializer.Serialize(value);
            _cache.StringSet(key, jsonData, expiration);
        }
        public T? Get<T>(string key)
        {
            string jsonData = _cache.StringGet(key);
            return jsonData != null ? JsonSerializer.Deserialize<T>(jsonData) : default;
        }
        public void Remove(string key)
        {
            _cache.KeyDelete(key);
        }
    }
}
