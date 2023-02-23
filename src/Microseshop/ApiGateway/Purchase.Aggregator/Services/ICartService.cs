using Purchase.Aggregator.Models;

namespace Purchase.Aggregator.Services
{
    public interface ICartService
    {
        Task<CartModel> GetBasket(string userName);
    }
}
