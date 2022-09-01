using Microsoft.AspNetCore.Mvc;

namespace ShopForEverything.Controllers
{
    public class ExchangeStockController : Controller
    {
        public IActionResult HomePageExchangeStock()
        {
            return View();
        }
    }
}
