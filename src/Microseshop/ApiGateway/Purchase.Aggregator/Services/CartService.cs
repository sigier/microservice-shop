using Purchase.Aggregator.Extensions;
using Purchase.Aggregator.Models;

namespace Purchase.Aggregator.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;

        public CartService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CartModel> GetBasket(string userName)
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/Basket/{userName}");
            return await response.ReadContentAs<CartModel>();
        }
    }
}
