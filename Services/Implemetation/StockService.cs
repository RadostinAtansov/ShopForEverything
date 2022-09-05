using Data;
using Data.Model.ShopEverything;
using Services.IShopServices;
using Services.Model.ShopEverything;

namespace Services.Implemetation
{
    public class StockService : IStockService
    {

        private readonly ShopEverythingDbContext data;

        public StockService(ShopEverythingDbContext data)
        {
            this.data = data;
        }

        public void AddStock(AddStockServiceViewModel stock)
        {
            var stockAdd = new Stock
            {
                 Size = stock.Size,
                 Name = stock.Name,
                 Color = stock.Color,
                 Price = stock.Price,
                 Picture = stock.Picture,
                 Description = stock.Description,
                 StockNumber = stock.StockNumber,
            };

            this.data.Stocks.Add(stockAdd);
            this.data.SaveChanges();
        }
    }
}
