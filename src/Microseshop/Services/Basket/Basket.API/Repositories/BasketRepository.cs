using Basket.API.Entities;
using Newtonsoft.Json;
using StackExchange.Redis;


namespace Basket.API.Repositories
{
    public class BasketRepository: IBasketRepository
    {
        private readonly IDatabase _redisDatabase;

        public BasketRepository(IConnectionMultiplexer redisDatabase)
        {
            _redisDatabase = redisDatabase.GetDatabase();
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisDatabase.StringSetAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            string basket = await _redisDatabase.StringGetAsync(userName);

            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }


        public async Task DeleteBasket(string userName)
        { 
            await _redisDatabase.KeyDeleteAsync(userName);
        }
    }
}
