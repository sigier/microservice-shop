namespace Purchase.Aggregator.Models
{
    public class RootShoppingModel
    {
        public string UserName { get; set; }
        public CartModel BasketWithProducts { get; set; }
        public IEnumerable<OrderResponseModel> Orders { get; set; }
    }
}
