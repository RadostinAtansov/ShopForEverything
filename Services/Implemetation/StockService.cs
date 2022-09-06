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

        public void AddStock(AddStockServiceViewModel stock, string path)
        {
            var stockAdd = new Stock
            {
                 Size = stock.Size,
                 Name = stock.Name,
                 Color = stock.Color,
                 Price = stock.Price,
                 Picture = path,
                 Description = stock.Description,
                 StockNumber = stock.StockNumber,
            };

            this.data.Stocks.Add(stockAdd);
            this.data.SaveChanges();
        }

        ICollection<ShowAllStockServiceViewModel> IStockService.ShowAllStocks()
        {
            var stocks = this.data.Stocks
            .Select(x => new ShowAllStockServiceViewModel
            {
                Name = x.Name,
                Size = x.Size,
                Color = x.Color,
                Description = x.Description,
                Picture = x.Picture,
                Price = x.Price,
                StockNumber = x.StockNumber,
            })
            .ToList();

            return stocks;
        }
    }
}
