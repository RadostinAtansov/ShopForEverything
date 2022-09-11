using Data;
using Services.IShopServices;
using Microsoft.AspNetCore.Mvc;
using Services.Model.ShopEverything;
using Microsoft.AspNetCore.Authorization;
using ShopForEverything.Models;
using Microsoft.AspNetCore.Identity;

namespace ShopForEverything.Controllers
{
    [Authorize]
    public class EverythingController : Controller
    {
        private readonly IStockService stockService;
        private readonly ShopEverythingDbContext data;
        private readonly HttpContext httpContext;
        private readonly UserManager<IdentityUser> userMaganer;

        public IWebHostEnvironment WebHostEnvironment;

        public EverythingController(IStockService stockService, 
            ShopEverythingDbContext data,
            IWebHostEnvironment webHostEnvironment,
            UserManager<IdentityUser> userMaganer)
        {
            this.data = data;
            this.stockService = stockService;
            WebHostEnvironment = webHostEnvironment;
            this.userMaganer = userMaganer;
        }

        public async Task<IActionResult> MyStocks(int pg = 1)
        {
           var allUserStocks = await this.stockService.ShowAllMyStocks(HttpContext);

            const int pageSize = 6;
            if (pg < 1)
                pg = 1;

            int stockCount = allUserStocks.Count();

            var pager = new Pager(stockCount, pg, pageSize);

            int stockSkip = (pg - 1) * pageSize;

            var data = allUserStocks.Skip(stockSkip).Take(pager.PageSize)
                .ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        public IActionResult ShowAllStocks(int pg = 1)
        {
            var allStocks = this.stockService.ShowAllStocks();

            const int pageSize = 6;
            if (pg < 1)
                pg = 1;

            int stockCount = allStocks.Count();

            var pager = new Pager(stockCount, pg, pageSize);

            int stockSkip = (pg - 1) * pageSize;

            var data = allStocks.Skip(stockSkip).Take(pager.PageSize)
                .ToList();

            this.ViewBag.Pager = pager;

            return View(data);


        }
        public IActionResult FavoriteStock()
        {
            return View();
        }

        public IActionResult Bag()
        {
            return View();
        }

 
        public IActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStock(AddStockServiceViewModel stock)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string stringFile = UploadFile(stock.Picture);

            await this.stockService.AddStock(stock, stringFile, HttpContext);

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
