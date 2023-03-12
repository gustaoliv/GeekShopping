using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductViewModel>> FindAllProducts(string token);
        public Task<ProductViewModel> FindProductById(long id, string token);
        public Task<ProductViewModel> CreateProduct(ProductViewModel model, string token);
        public Task<ProductViewModel> UpdateProduct(ProductViewModel model, string token);
        public Task<bool> DeleteProductById(long id, string token);
    }
}
