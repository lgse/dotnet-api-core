using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

#nullable enable

namespace API.Core.Cache.StackExchange
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly ILogger _logger;
        private readonly IConnectionMultiplexer _multiplexer;

        private async Task<bool> AcquireLockAsync(string key, string value, TimeSpan expiration)
        {
            bool flag;

            try
            {
                flag = await _multiplexer.GetDatabase().StringSetAsync(key, value, expiration, When.NotExists);
            }
            catch (Exception e)
            {
                flag = true;
                _logger.LogInformation("Could not acquire lock: {Message}", e.Message);
            }

            return flag;
        }

        public RedisCacheProvider(IConnectionMultiplexer multiplexer, ILoggerFactory loggerFactory)
        {
            _multiplexer = multiplexer;
            _logger = loggerFactory.CreateLogger<RedisCacheProvider>();
        }

        public async Task<bool> StoreAsync<T>(string key, T obj, TimeSpan? expiry = null)
        {
            var db = _multiplexer.GetDatabase();
            var serializedObject = JsonSerializer.Serialize(obj);

            try {
                return await db.StringSetAsync(key, serializedObject, expiry);
            }
            catch (Exception e) {
                _logger.LogError(
                    "Could not store object: {Message} [{InnerMessage}],",
                    e.Message,
                    e.InnerException?.Message
                );

                return false;
            }
        }

        public async Task<bool> StoreMultipleAsync<T>(Dictionary<string, T> objects)
        {
            var db = _multiplexer.GetDatabase();
            var serializedObjects = new List<KeyValuePair<RedisKey, RedisValue>>();

            foreach (var key in objects.Keys) {
                var serializedObject = JsonSerializer.Serialize(objects.GetValueOrDefault(key));

                serializedObjects.Add(new KeyValuePair<RedisKey, RedisValue>(key, serializedObject));
            }

            try {
                return await db.StringSetAsync(serializedObjects.ToArray());
            }
            catch (Exception e) {
                _logger.LogError(
                    "Could not store object: {Message} [{InnerMessage}],",
                    e.Message,
                    e.InnerException?.Message
                );

                return false;
            }
        }

        public async Task<T?> RetrieveAsync<T>(string key)
        {
            var db = _multiplexer.GetDatabase();

            try {
                var result = await db.StringGetAsync(key);
                return result.IsNull ? default : JsonSerializer.Deserialize<T>(result.ToString());
            }
            catch (Exception e) {
                _logger.LogError(
                    "Could not retrieve object: {Message} [{InnerMessage}],",
                    e.Message,
                    e.InnerException?.Message
                );

                return default;
            }
        }

        public async Task<List<T>> RetrieveMultipleAsync<T>(IEnumerable<string> keys)
        {
            var db = _multiplexer.GetDatabase();
            var results = new List<T>();

            try {
                var redisKeys = keys.Select(k => new RedisKey(k)).ToArray();

                foreach (var obj in await db.StringGetAsync(redisKeys)) {
                    if (!obj.IsNull) {
                        var cast = JsonSerializer.Deserialize<T>(obj);

                        if (cast != null) {
                            results.Add(cast);
                        }
                    }
                }
            }
            catch (Exception e) {
                _logger.LogError(
                    "Could not retrieve objects: {Message} [{InnerMessage}],",
                    e.Message,
                    e.InnerException?.Message
                );
            }

            return results;
        }

        public async Task DeleteAsync(string key)
        {
            var db = _multiplexer.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }
}
