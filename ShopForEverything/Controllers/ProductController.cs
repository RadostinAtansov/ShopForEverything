using Data;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ShopForEverything.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Services.IShopServices;

namespace ShopForEverything.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShopEverythingDbContext data;
        private readonly IProductService productService;

        public ProductController(ShopEverythingDbContext data, IProductService productService)
        {
            this.data = data;
            this.productService = productService;
        }


        public IActionResult Index()
        {
            var products = this.productService.GetProducts();
            return View(products);
        }
    }

}