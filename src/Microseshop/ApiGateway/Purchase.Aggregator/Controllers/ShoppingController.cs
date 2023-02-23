using Microsoft.AspNetCore.Mvc;
using System.Net;
using Purchase.Aggregator.Models;
using Purchase.Aggregator.Services;


namespace Purchase.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogueService _catalogueService;
        private readonly ICartService _basketService;
        private readonly IOrderService _orderService;

        public ShoppingController(ICatalogueService catalogueService, ICartService basketService, IOrderService orderService)
        {
            _catalogueService = catalogueService ?? throw new ArgumentNullException(nameof(catalogueService)); 
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService)); 
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService)); 
        }


        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(RootShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RootShoppingModel>> GetShopping(string userName)
        {
      

            CartModel basket = await _basketService.GetBasket(userName);

            foreach (CartItemModel item in basket.Items)
            {
                CatalogueModel product = await _catalogueService.GetCatalog(item.ProductId);

                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            IEnumerable<OrderResponseModel> orders = await _orderService.GetOrdersByUserName(userName);

            RootShoppingModel shoppingModel = new RootShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return Ok(shoppingModel);
        }
    }
}
