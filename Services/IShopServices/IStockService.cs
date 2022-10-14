using Services.Model.ShopEverything;
using Microsoft.AspNetCore.Http;
using ShopForEverything.Models;

namespace Services.IShopServices
{
    public interface IStockService
    {
        void DeleteMyStock(string id);
        Task RemoveFromMyFavorite(string id, HttpContext httpContext);
        EditMyStockServiceViewModel EditMyStock(string id);
        ShowStockDetailsServiceViewModel DetailsStock(string id);
        DetailsMyStockServiceViewModel MyStockDetails(string id);
        ICollection<ShowAllStockServiceViewModel> ShowAllStocks();
        DetailsMyStockServiceViewModel MyFavoriteStockDetails(string id);
        Task AddStock(AddStockServiceViewModel model, string path, HttpContext httpContext);
        Task<List<ShowAllFavoriteUserStocksServiceViewModel>> ShowAllMyStocks(HttpContext httpContext);
        Task<List<ShowAllFavoriteUserStocksServiceViewModel>> ShowAllMyFavoriteStocks(HttpContext httpContext);
        ICollection<ShowAllStockServiceViewModel> SearchByWord(string searchWord);
    }
}
