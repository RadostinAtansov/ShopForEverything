using Data;
using Services.IShopServices;
using ShopForEverything.Models.Cart;

namespace ShopForEverything.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopEverythingDbContext data;

        public ProductService(ShopEverythingDbContext data)
        {
            this.data = data;
        }

        public Product GetProduct(string id)
        {
            return GetProducts()
                .Where(s => s.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetProducts()
        {
            return this.data.Stocks.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Picture = p.Picture
            })
            .ToList();
        }
    }
}
