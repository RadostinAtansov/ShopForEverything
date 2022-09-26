using Data;
using Services.IShopServices;
using Microsoft.AspNetCore.Mvc;
using Xceed.Wpf.Toolkit;
using ShopForEverything.Models;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Data.Model.ShopEverything;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Model.ShopEverything;
using Microsoft.AspNetCore.Authorization;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace ShopForEverything.Controllers
{
    [Authorize]
    public class EverythingController : Controller
    {
        private readonly IStockService stockService;
        private readonly ShopEverythingDbContext data;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;

        public IWebHostEnvironment WebHostEnvironment;

        public EverythingController(IStockService stockService, 
            ShopEverythingDbContext data,
            IWebHostEnvironment webHostEnvironment,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.data = data;
            this.userManager = userManager;
            this.stockService = stockService;
            WebHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }


        public IActionResult MyStockDetails(string id)
        {
            var stock = this.stockService.MyStockDetails(id);

            return View(stock);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult ShowDetailsStock(string id)
        {
            var stock = this.stockService.DetailsStock(id);

            return View(stock);
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

        public async Task<IActionResult> MyFavorite(int pg = 1)
        {

            //When put this throught service it dosn't work

            //var favStock = this.stockService.ShowAllMyFavoriteStocks(HttpContext);

            var userName = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            var name = userName.UserName;

            var user = this.data.Users
                .FirstOrDefault(u => u.UserName == name);

            var favStock = this.data.UserFavoriteStocks
                .Where(a => a.ApplicationUserId == user.Id)
                .Select(s => new ShowAllFavoriteUserStocksServiceViewModel
                {
                    Id = s.FavoriteStock.Id,
                    Color = s.FavoriteStock.Color,
                    Description = s.FavoriteStock.Description,
                    Name = s.FavoriteStock.Name,
                    Picture = s.FavoriteStock.Picture,
                    Price = s.FavoriteStock.Price,
                    Size = s.FavoriteStock.Size,
                    StockNumber = s.FavoriteStock.StockNumber,
                })
                .ToList();

                const int pageSize = 6;
                if (pg < 1)
                    pg = 1;

                int stockCount = favStock.Count();

                var pager = new Pager(stockCount, pg, pageSize);

                int stockSkip = (pg - 1) * pageSize;

                var data = favStock.Skip(stockSkip).Take(pager.PageSize)
                    .ToList();

                this.ViewBag.Pager = pager;


            if (data.Count() == null)
            {
                return View();
            }

            return View(data);

        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> AddToFavoriteStock(string id)
        {
            var userName = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            var name = userName.UserName;

            var stock = this.data.Stocks
                .FirstOrDefault(s => s.Id == id);

            var userFavStock = new UserFavoriteStocks()
            {
                ApplicationUserId = userName.Id,
                FavoriteStockId = stock.Id,
            };

            try
            { 
                await this.data.UserFavoriteStocks.AddAsync(userFavStock);
                await this.data.SaveChangesAsync();
            }
            catch (Exception)
            {
                TempData["AlertMessageNegative"] = "Negative, Cant Add to favorite two times";
                return RedirectToAction("ShowAllStocks", "Everything");
            }
            finally
            {
                TempData["AlertMessageSuccessefully"] = "Successefully Added to favorite";
            }
            return RedirectToAction("ShowAllStocks", "Everything");

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
