using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IShopServices;
using ShopForEverything.Extension;
using ShopForEverything.Models.Cart;

namespace ShopForEverything.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            this._productService = productService;
        }

        public IActionResult Index()
        {

            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");

            if (cart != null)
            {
                ViewBag.total = cart.Sum(s => s.Quantity * s.Product.Price);
            }
            else
            {
                cart = new List<Product_Item>();
                ViewBag.total = 0;
            }

            return View(cart);
        }

        public IActionResult Buy(string id)
        {
            var product = this._productService.GetProduct(id);
            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");

            if (cart == null)
            {
                cart = new List<Product_Item>();
                cart.Add(new Product_Item { Product = product, Quantity = 1});
            }
            else
            {
                int index = cart.FindIndex(p => p.Product.Id == id);

                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Product_Item { Product = product, Quantity = 1});
                }
            }

            HttpContext.Session.Set<List<Product_Item>>("cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Add(string id)
        {
            var product = this._productService.GetProduct(id);
            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");

            int index = cart.FindIndex(a => a.Product.Id == id);
            cart[index].Quantity++;

            HttpContext.Session.Set<List<Product_Item>>("cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Minus(string id)
        {
            var product = this._productService.GetProduct(id);
            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");

            int index = cart.FindIndex(s => s.Product.Id == id);

            if (cart[index].Quantity == 1)
            {
                cart.RemoveAt(index);
            }
            else
            {
                cart[index].Quantity--;
            }

            HttpContext.Session.Set<List<Product_Item>>("cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(string id)
        {
            var product = this._productService.GetProduct(id);
            var cart = HttpContext.Session.Get<List<Product_Item>>("cart");

            int index = cart.FindIndex(s => s.Product.Id == id);
            cart.RemoveAt(index);

            HttpContext.Session.Set<List<Product_Item>>("cart", cart);

            return RedirectToAction("Index");
        }
    }
}
