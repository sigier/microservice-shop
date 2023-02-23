using Purchase.Aggregator.Extensions;
using Purchase.Aggregator.Models;

namespace Purchase.Aggregator.Services
{
    public class CatalogueService : ICatalogueService
    {
        private readonly HttpClient _client;

        public CatalogueService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CatalogueModel>> GetCatalog()
        {
            HttpResponseMessage response = await _client.GetAsync("/api/v1/Catalog");
            return await response.ReadContentAs<List<CatalogueModel>>();
        }

        public async Task<IEnumerable<CatalogueModel>> GetCatalogByCategory(string category)
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/Catalog/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<CatalogueModel>>();
        }

        public async Task<CatalogueModel> GetCatalog(string id)
        {
            HttpResponseMessage response = await _client.GetAsync($"/api/v1/Catalog/{id}");
            return await response.ReadContentAs<CatalogueModel>();
        }
    }
}
