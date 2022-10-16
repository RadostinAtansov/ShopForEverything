using Services.Model.ShopEverything;
using Microsoft.AspNetCore.Http;
using ShopForEverything.Models;

namespace Services.IShopServices
{
    public interface IStockService
    {
        void DeleteMyStock(string id);
        EditMyStockServiceViewModel EditMyStock(string id);
        ShowStockDetailsServiceViewModel DetailsStock(string id);
        DetailsMyStockServiceViewModel MyStockDetails(string id);
        ICollection<ShowAllStockServiceViewModel> ShowAllStocks();
        Task RemoveFromMyFavorite(string id, HttpContext httpContext);
        DetailsMyStockServiceViewModel MyFavoriteStockDetails(string id);
        ICollection<ShowAllStockServiceViewModel> SearchByWord(string searchWord);
        Task AddStock(AddStockServiceViewModel model, string path, HttpContext httpContext);
        Task<List<ShowAllFavoriteUserStocksServiceViewModel>> ShowAllMyStocks(HttpContext httpContext);
        Task<List<ShowAllFavoriteUserStocksServiceViewModel>> ShowAllMyFavoriteStocks(HttpContext httpContext);
    }
}
