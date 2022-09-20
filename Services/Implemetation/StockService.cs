using Data;
using Data.Model.ShopEverything;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.IShopServices;
using Services.Model.ShopEverything;
using ShopForEverything.Models;

namespace Services.Implemetation
{
    public class StockService : IStockService
    {

        private readonly ShopEverythingDbContext data;
        private readonly UserManager<IdentityUser> userManager;

        public StockService(ShopEverythingDbContext data, UserManager<IdentityUser> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        public async Task AddStock(AddStockServiceViewModel stock, string path, HttpContext httpContext)
        {

            var userName = await userManager.GetUserAsync(httpContext.User);
            var name = userName.UserName;

            var stockAdd = new Stock
            {
                Size = stock.Size,
                Name = stock.Name,
                Color = stock.Color,
                Price = stock.Price,
                Picture = path,
                Description = stock.Description,
                StockNumber = stock.StockNumber,
                AddedFromUser = name,
            };

            await this.data.Stocks.AddAsync(stockAdd);
            await this.data.SaveChangesAsync();
        }

        public ShowStockDetailsServiceViewModel DetailsStock(string id)
        {
            var stock = this.data.Stocks
                .Where(k => k.Id == id)
                .Select(v => new ShowStockDetailsServiceViewModel
                {
                    Name = v.Name,
                    Size = v.Size,
                    Color = v.Color,
                    Price = v.Price,
                    Picture = v.Picture,
                    Description = v.Description,
                    StockNumber = v.StockNumber,
                    AddedFromUser = v.AddedFromUser,
                })
                .FirstOrDefault();
                

            return stock;

        }

        public async Task<List<ShowAllFavoriteUserStocksServiceViewModel>> ShowAllMyFavoriteStocks(HttpContext httpContext)
        {
            //var userName = await userManager.GetUserAsync(httpContext.User);
            //var name = userName.UserName;

            //var user = this.data.Users
            //    .FirstOrDefault(u => u.UserName == name);

            //var favStock = this.data.UserFavoriteStocks
            //    .Where(a => a.ApplicationUserId == user.Id)
            //    .Select(s => new ShowAllFavoriteUserStocksServiceViewModel
            //    {
            //        Color = s.FavoriteStock.Color,
            //        Description = s.FavoriteStock.Description,
            //        Name = s.FavoriteStock.Name,
            //        Picture = s.FavoriteStock.Picture,
            //        Price = s.FavoriteStock.Price,
            //        Size = s.FavoriteStock.Size,
            //        StockNumber = s.FavoriteStock.StockNumber,
            //    })
            //    .ToList();

            return null;
        }

        public async Task<List<ShowAllFavoriteUserStocksServiceViewModel>> ShowAllMyStocks(HttpContext httpContext)
        {
            var userName = await userManager.GetUserAsync(httpContext.User);
            var name = userName.UserName;

            var allUserStocks = this.data.Stocks
                .Where(x => x.AddedFromUser == name)
                .Select(u => new ShowAllFavoriteUserStocksServiceViewModel
                {
                    Name = u.Name,
                    Size = u.Size,
                    Color = u.Color,
                    Price = u.Price,
                    Picture = u.Picture,
                    Description = u.Description,
                    StockNumber = u.StockNumber,
                })
                .ToList();

            return allUserStocks;
        }

        ICollection<ShowAllStockServiceViewModel> IStockService.ShowAllStocks()
        {
            var stocks = this.data.Stocks
            .Select(x => new ShowAllStockServiceViewModel
            {
                Id = x.Id,
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
