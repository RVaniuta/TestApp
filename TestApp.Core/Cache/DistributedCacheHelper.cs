using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using TestApp.Core.Cache.Interfaces;
using Utf8Json;

namespace TestApp.Core.Cache
{
    public class DistributedCacheHelper : ICacheHelper
    {
        private const string _cachePrefix = "cached_";

        private readonly IDistributedCache _cache;

        public DistributedCacheHelper(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetFromCacheAsync<T>(Func<Task<T>> action, string key, int expirationSeconds) where T : class
        {
            var item = await Get<T>(key);

            if (item != null)
                return item;

            T result = await action();

            await Set(result, key, expirationSeconds);
            return result;
        }

        public async Task<T> GetFromCacheAsync<T>(Func<T> action, string key, int expirationSeconds) where T : class
        {
            var item = await Get<T>(key);

            if (item != null)
                return item;

            T result = action();

            await Set(result, key, expirationSeconds);
            return result;
        }

        public async Task<T> Get<T>(string key)
        {
            var cachedItem = await _cache.GetStringAsync(CacheKey(key));

            if (!string.IsNullOrEmpty(cachedItem))
            {
                return JsonSerializer.Deserialize<T>(cachedItem);
            }

            return default;
        }

        public async Task Set<T>(T item, string key, int expirationSeconds)
        {
            var jsonItem = JsonSerializer.ToJsonString(item);

            await _cache.SetStringAsync(CacheKey(key), jsonItem, new DistributedCacheEntryOptions { 
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirationSeconds)
            });
        }

        private string CacheKey(string key) => $"{_cachePrefix}{key}";
    }
}
