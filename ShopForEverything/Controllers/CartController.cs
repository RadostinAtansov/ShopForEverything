using Data.Model.ShopEverything.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Web.Providers.Entities;

namespace ShopForEverything.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        

        public IActionResult Buy(string id)
        {
            ProductModel productModel = new ProductModel();

            Session session = new Session();

            if (Session["cart"] == null)
            {

            }

            return RedirectToAction("Index");
        }
    }

