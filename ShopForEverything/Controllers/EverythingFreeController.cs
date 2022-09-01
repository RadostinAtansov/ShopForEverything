using Microsoft.AspNetCore.Mvc;

namespace ShopForEverything.Controllers
{
    public class EverythingFreeController : Controller
    {
        public IActionResult HomePageEverythingFree()
        {
            return View();
        }
    }
}
