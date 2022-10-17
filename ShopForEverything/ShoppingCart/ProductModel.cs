namespace Data.Model.ShopEverything.ShoppingCart
{
    public class ProductModel
    {
        private readonly ShopEverythingDbContext data;
        private List<Product> products;

        public ProductModel()
        {
            this.data = data;
        }

        public ICollection<Product> FindAll()
        {
            this.products =  this.data.Stocks
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Photo = p.Picture,
                })
                .ToList();

            return this.products;
        }

        public Product Find(string id)
        {
            return this.products.FirstOrDefault(p => p.Id == id);
        }

    }
}
