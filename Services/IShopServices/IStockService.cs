using Services.Model.ShopEverything;

namespace Services.IShopServices
{
    public interface IStockService
    {
        void AddStock(AddStockServiceViewModel model, string path);
        ICollection<ShowAllStockServiceViewModel> ShowAllStocks();
    }
}
