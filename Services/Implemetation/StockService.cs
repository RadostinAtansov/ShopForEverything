using Data;
using Data.Model.ShopEverything;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.IShopServices;
using Services.Model.ShopEverything;
using ShopForEverything.Models;
using System.Web.Mvc;

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
                Id = stock.Id,
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

        public DetailsMyStockServiceViewModel MyStockDetails(string id)
        {
            var myStock = this.data.Stocks
                .Where(k => k.Id == id)
                .Select(a => new DetailsMyStockServiceViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Size = a.Size,
                    Color = a.Color,
                    Price = a.Price,
                    Picture = a.Picture,
                    Description = a.Description,
                    StockNumber = a.StockNumber,
                })
                .FirstOrDefault();


            return myStock;
        }

        public DetailsMyStockServiceViewModel MyFavoriteStockDetails(string id)
        {
            var myStock = this.data.Stocks
                .Where(k => k.Id == id)
                .Select(a => new DetailsMyStockServiceViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Size = a.Size,
                    Color = a.Color,
                    Price = a.Price,
                    Picture = a.Picture,
                    Description = a.Description,
                    StockNumber = a.StockNumber,
                })
                .FirstOrDefault();


            return myStock;
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

         public async Task RemoveFromMyFavorite(string id, HttpContext httpContext)
        {
            //var user = await userManager.GetUserAsync(httpContext.User);
            //var name = user.UserName;

            //var userN = this.data.UserFavoriteStocks
            //    .FirstOrDefault(u => u.ApplicationUserId == user.Id)
            // ;


            ////stock.FavoriteStock = null;
            //var userStock = new UserFavoriteStocks()
            //{
            //    ApplicationUserId = user.Id,
            //    FavoriteStockId = id,
            //};

            //userStock.FavoriteStockId = null;

            //this.data.UserFavoriteStocks.Add(stock);
            this.data.SaveChanges();
        }

        public async Task<List<ShowAllFavoriteUserStocksServiceViewModel>> ShowAllMyStocks(HttpContext httpContext)
        {
            var userName = await userManager.GetUserAsync(httpContext.User);
            var name = userName.UserName;

            var allUserStocks = this.data.Stocks
                .Where(x => x.AddedFromUser == name)
                .Select(u => new ShowAllFavoriteUserStocksServiceViewModel
                {
                    Id = u.Id,
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

        public void DeleteMyStock(string id)
        {
            var myStock = this.data.Stocks.Find(id);

            var stockFavUser = this.data
                .UserFavoriteStocks
                .Where(a => a.FavoriteStockId == id)
                .ToList();

            for (int i = 0; i < stockFavUser.Count; i++)
            {
                this.data.UserFavoriteStocks.Remove(stockFavUser[i]);
            }


            this.data.Stocks.Remove(myStock);
            this.data.SaveChanges();
        }

        public EditMyStockServiceViewModel EditMyStock(string id)
        {
            var stock = this.data.Stocks
                .Select(s => new EditMyStockServiceViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Size = s.Size,
                    Price = s.Price,
                    Color = s.Color,
                    Picture= s.Picture,
                    StockNumber= s.StockNumber,
                    Description= s.Description,
                    AddFromUser = s.AddedFromUser,
                })
                .FirstOrDefault();

            return stock;
        }

        public ICollection<ShowAllStockServiceViewModel> SearchByWord(string searchWord)
        {

            var searchResult = this.data.Stocks
                .Where(s =>
                s.Name.Contains(searchWord) ||
                s.Name.StartsWith(searchWord) ||
                s.Name.EndsWith(searchWord))
                .Select(s => new ShowAllStockServiceViewModel
                {
                    Name = s.Name,
                    AddedFromUser = s.AddedFromUser,
                    Color = s.Color,
                    Description = s.Description,
                    Picture = s.Picture,
                    Price = s.Price,
                    Size = s.Size,
                    StockNumber = s.StockNumber,
                })
                .ToList();

            return searchResult;
        }
    }
}
