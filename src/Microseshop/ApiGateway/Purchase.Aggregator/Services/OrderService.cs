using Purchase.Aggregator.Extensions;
using Purchase.Aggregator.Models;

namespace Purchase.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/Order/{userName}");
            return await response.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
