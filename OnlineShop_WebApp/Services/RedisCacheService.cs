using StackExchange.Redis;

namespace OnlineShop_WebApp.Services
{
    public class RedisCacheService
    {
        private readonly IConnectionMultiplexer redis;
        private readonly SemaphoreSlim mutex = new SemaphoreSlim(1, 1);

        public RedisCacheService(IConnectionMultiplexer redis) 
        {
            this.redis = redis;
        }

        public async Task SetAsync(string key, string value)
        {
            await mutex.WaitAsync();
            try
            {
                var database = redis.GetDatabase();
                await database.StringSetAsync(key, value);
            }
            catch
            {
                return;
            }
            finally
            {
                mutex.Release();
            }
        }

        public async Task<string> TryGetAsync(string key) 
        {
            
            try 
            {
                var database = redis.GetDatabase();
                return await database.StringGetAsync(key);
            }
            catch 
            {
                return null;
            }
        }

        public async Task RemoveAsync(string key) 
        {
            try {
                var database = redis.GetDatabase();
                await database.KeyDeleteAsync(key);
            }
            catch 
            {
                return;
            }
        }
    }
}
