using Data;
using Services.IShopServices;
using Microsoft.AspNetCore.Mvc;
using Services.Model.ShopEverything;
using Microsoft.AspNetCore.Authorization;
using ShopForEverything.Models;

namespace ShopForEverything.Controllers
{
    [Authorize]
    public class EverythingController : Controller
    {
        private readonly IStockService stockService;
        private readonly ShopEverythingDbContext data;
        public IWebHostEnvironment WebHostEnvironment;

        public EverythingController(IStockService stockService, 
            ShopEverythingDbContext data,
            IWebHostEnvironment webHostEnvironment)
        {
            this.data = data;
            this.stockService = stockService;
            WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult MyStocks()
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

        public IActionResult ShowAllStocks(int pg = 1)
        {
            var allStocks = this.stockService.ShowAllStocks();

            const int pageSize = 6;
            if (pg < 1)
                pg = 1;

            int recsCount = allStocks.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recsSkip = (pg - 1) * pageSize;

            var data = allStocks.Skip(recsSkip).Take(pager.PageSize)
                .ToList();

            this.ViewBag.Pager = pager;

            //return View(allStocks);
            return View(data);


        }
 
        public IActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStock(AddStockServiceViewModel stock)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string stringFile = UploadFile(stock.Picture);

            this.stockService.AddStock(stock, stringFile);

            return RedirectToAction("Index", "Home");
        }

        private string UploadFile(IFormFile model)
        {
            string fileName = null;
            if (model != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + model.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.CopyTo(fileStream);
            }
            return fileName;
        }
    }
}
