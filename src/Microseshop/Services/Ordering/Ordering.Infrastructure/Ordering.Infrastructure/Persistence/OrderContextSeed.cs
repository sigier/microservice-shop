using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure
{
    public static class OrderContextSeed
    {
        public static void SeedData(OrderContext context)
        {
            if (!context.Orders.Any())
            {
                context.Orders.AddRange(GetPreconfiguredOrders());
                context.SaveChanges();
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Steve", LastName = "Lozik", EmailAddress = "testfake@gmail.com", AddressLine = "Street", Country = "UK", TotalPrice = 350 }
            };
        }
    }
}
