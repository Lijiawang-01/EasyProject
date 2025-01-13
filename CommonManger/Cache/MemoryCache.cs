using CommonManager.Helper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EasyWechatModels.Enum.CacheEnum;

namespace CommonManager.Cache
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class MemoryCache : IMemoryCache
    {
        readonly IDistributedCache _cache;
        public MemoryCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        protected string BuildKey(string idKey)
        {
            return $"Cache_{GetType().FullName}_{idKey}";
        }
        public void SetCache(string key, object value)
        {
            string cacheKey = BuildKey(key);
            _cache.SetString(cacheKey, value.ToJson());
        }

        public async Task SetCacheAsync(string key, object value)
        {
            string cacheKey = BuildKey(key);
            await _cache.SetStringAsync(cacheKey, value.ToJson());
        }

        public void SetCache(string key, object value, TimeSpan timeout)
        {
            string cacheKey = BuildKey(key);
            _cache.SetString(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
            });
        }

        public async Task SetCacheAsync(string key, object value, TimeSpan timeout)
        {
            string cacheKey = BuildKey(key);
            await _cache.SetStringAsync(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
            });
        }

        public void SetCache(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            string cacheKey = BuildKey(key);
            if (expireType == ExpireType.Absolute)
            {
                //这里没转换标准时间，Linux时区会有问题？
                _cache.SetString(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
                });
            }
            else
            {
                _cache.SetString(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeout
                });
            }
        }

        public async Task SetCacheAsync(string key, object value, TimeSpan timeout, ExpireType expireType)
        {
            string cacheKey = BuildKey(key);
            if (expireType == ExpireType.Absolute)
            {
                //这里没转换标准时间，Linux时区会有问题？
                await _cache.SetStringAsync(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now + timeout)
                });
            }
            else
            {
                await _cache.SetStringAsync(cacheKey, value.ToJson(), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeout
                });
            }
        }

        public string GetCache(string idKey)
        {
            if (string.IsNullOrEmpty(idKey))
            {
                return null;
            }
            string cacheKey = BuildKey(idKey);
            var cache = _cache.GetString(cacheKey);
            return cache;
        }
        public async Task<string> GetCacheAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            string cacheKey = BuildKey(key);
            var cache = await _cache.GetStringAsync(cacheKey);
            return cache;
        }

        public T GetCache<T>(string key)
        {
            var cache = GetCache(key);
            if (!string.IsNullOrEmpty(key))
            {
                return cache.ToObject<T>();
            }
            return default(T);
        }

        public async Task<T> GetCacheAsync<T>(string key)
        {
            var cache = await GetCacheAsync(key);
            if (!string.IsNullOrEmpty(cache))
            {
                return cache.ToObject<T>();
            }
            return default(T);
        }

        public void RemoveCache(string key)
        {
            _cache.Remove(BuildKey(key));
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cache.RemoveAsync(BuildKey(key));
        }

        public void RefreshCache(string key)
        {
            _cache.Refresh(BuildKey(key));
        }

        public async Task RefreshCacheAsync(string key)
        {
            await _cache.RefreshAsync(BuildKey(key));
        }

    }
}
