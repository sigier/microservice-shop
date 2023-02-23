namespace Purchase.Aggregator.Models
{
    public class CartModel
    {
        public string UserName { get; set; }
        public List<CartItemModel> Items { get; set; } = new List<CartItemModel>();
        public decimal TotalPrice { get; set; }
    }
}
