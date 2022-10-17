using Data.Model.ShopEverything.ShoppingCart;
using Microsoft.AspNetCore.Mvc;

namespace ShopForEverything.Controllers
{
    public class ProductController : Controller
    {

        public IActionResult Index()
        {
            ProductModel productModel = new ProductModel();
            ViewBag.products = productModel.FindAll();

            return View();
        }
    }
}
