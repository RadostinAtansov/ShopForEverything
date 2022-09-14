namespace Data.Model.ShopEverything
{
    public class FavoriteStock
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserFavoriteStocks> ApplicationUsers { get; set; } = new HashSet<UserFavoriteStocks>();
    }
}