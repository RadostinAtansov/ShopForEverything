using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.IShopServices;
using Services.Model.ShopEverything;

namespace ShopForEverything.Controllers
{
    [Authorize]
    public class EverythingController : Controller
    {
        private readonly IStockService stockService;
        private readonly ShopEverythingDbContext data;

        public EverythingController(IStockService stockService, ShopEverythingDbContext data)
        {
            this.data = data;
            this.stockService = stockService;
        }

        public IActionResult AllStocks()
        {
            return View();
        }


 
        public IActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStock(AddStockServiceViewModel stock)
        {
            this.stockService.AddStock(stock);

            return View();
        }

        public IActionResult FavoriteStock()
        {
            return View();
        }

        public IActionResult Bag()
        {
            return View();
        }
    }
}
