using Microsoft.AspNetCore.Http;

namespace Services.Model.ShopEverything
{
    public class DetailsMyStockServiceViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }

        public string StockNumber { get; set; }

        public string Color { get; set; }

        public string Picture { get; set; }
    }
}
