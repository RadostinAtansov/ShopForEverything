using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopForEverything.Controllers
{
    [Authorize]
    public class EverythingController : Controller
    {
        public IActionResult AllStocks()
        {
            return View();
        }

        public IActionResult AddStock()
        {
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
