
using Microsoft.AspNetCore.Identity;

namespace Data.Model.ShopEverything
{
    public class AppUser : IdentityUser
    {
        public int StockIdto { get; set; }
    }
}
