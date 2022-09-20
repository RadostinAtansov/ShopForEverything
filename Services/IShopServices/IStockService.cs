using Services.Model.ShopEverything;
using Microsoft.AspNetCore.Http;
using ShopForEverything.Models;

namespace Services.IShopServices
{
    public interface IStockService
    {
        Task AddStock(AddStockServiceViewModel model, string path, HttpContext httpContext);
        ICollection<ShowAllStockServiceViewModel> ShowAllStocks();
        Task<List<ShowAllFavoriteUserStocksServiceViewModel>> ShowAllMyStocks(HttpContext httpContext);
        Task<List<ShowAllFavoriteUserStocksServiceViewModel>> ShowAllMyFavoriteStocks(HttpContext httpContext);
        ShowStockDetailsServiceViewModel DetailsStock(string id);
    }
}
