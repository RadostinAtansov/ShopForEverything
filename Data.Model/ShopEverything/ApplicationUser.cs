using Microsoft.AspNetCore.Identity;

namespace Data.Model.ShopEverything
{
    public class ApplicationUser : IdentityUser
    {
        public string? Id { get; set; }

        public ICollection<UserFavoriteStocks> FavoriteStocks { get; set; } = new HashSet<UserFavoriteStocks>();
    }
}