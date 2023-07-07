namespace Core.CacheServices
{
    public interface ICacheService
    {
        public T Set<T>(object key, T value, int expirationInMinutes = 60);
        public bool Contains(object key);
        public T Get<T>(object key);
        public void Clear(object key);
        public void Reset();
    }
}
