namespace OpenAI_Integration.Services
{
    using Microsoft.Extensions.Caching.Memory;

    using OpenAI_Integration.Interfaces;

    public class CacheService : ICacheService
    {
        private readonly IMemoryCache cache;

        public CacheService(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public string Get(string key)
        {
            return this.cache.Get<string>(key);
        }

        public void Set(string key, string value)
        {
            this.cache.Set(key, value);
        }

        public void Delete(string key)
        {
            this.cache.Remove(key);
        }
    }
}
