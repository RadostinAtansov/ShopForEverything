using ShopForEverything.Models.Cart;

namespace Services.IShopServices
{
    public interface IProductService
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProduct(string id);
    }
}
