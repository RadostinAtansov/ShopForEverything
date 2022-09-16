namespace Data.Model.ShopEverything
{
    public class UserFavoriteStocks
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string FavoriteStockId { get; set; }
        public Stock FavoriteStock { get; set; }
    }
}
