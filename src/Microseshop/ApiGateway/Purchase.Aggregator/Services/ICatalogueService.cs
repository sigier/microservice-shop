using Purchase.Aggregator.Models;

namespace Purchase.Aggregator.Services
{
    public interface ICatalogueService
    {
        Task<IEnumerable<CatalogueModel>> GetCatalog();
        Task<IEnumerable<CatalogueModel>> GetCatalogByCategory(string category);
        Task<CatalogueModel> GetCatalog(string id);
    }
}
