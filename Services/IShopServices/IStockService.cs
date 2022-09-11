﻿using Microsoft.AspNetCore.Http;
using Services.Model.ShopEverything;
using ShopForEverything.Models;

namespace Services.IShopServices
{
    public interface IStockService
    {
        Task AddStock(AddStockServiceViewModel model, string path, HttpContext httpContext);
        ICollection<ShowAllStockServiceViewModel> ShowAllStocks();
        Task<List<ShowAllFavoriteUserStocksServiceViewModel>> ShowAllMyStocks(HttpContext httpContext);
    }
}
