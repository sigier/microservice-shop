using Purchase.Aggregator.Models;

namespace Purchase.Aggregator.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);

    }
}
