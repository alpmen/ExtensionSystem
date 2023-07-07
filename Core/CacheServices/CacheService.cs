using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Core.CacheServices
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Set<T>(object key, T value, int expirationInMinutes = 60)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal).SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationInMinutes));

            options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
            _memoryCache.Set(key, value, options);
            return value;
        }
        /// <summary>
        /// checks for cache entry existence
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(object key)
        {
            return _memoryCache.TryGetValue(key, out object result);
        }
        /// <summary>
        /// returns cache entry T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(object key)
        {
            return _memoryCache.TryGetValue(key, out T result) ? result : default(T);
        }
        /// <summary>
        /// clear cache entry
        /// </summary>
        /// <param name="key"></param>
        public void Clear(object key)
        {
            _memoryCache.Remove(key);
        }
        /// <summary>
        /// expires cache entries T based on CancellationTokenSource cancel 
        /// </summary>
        public void Reset()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested &&
                _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }
            _resetCacheToken = new CancellationTokenSource();
        }
    }
}