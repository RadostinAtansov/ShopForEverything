using Data.Model.ShopEverything;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<UserFavoriteStocks>
    {
        public void Configure(EntityTypeBuilder<UserFavoriteStocks> userFavStock)
        {
            userFavStock.HasKey(k => new { k.ApplicationUserId, k.FavoriteStockId });

            userFavStock
                .HasOne(u => u.ApplicationUser)
                .WithMany(u => u.FavoriteStocks)
                .HasForeignKey(u => u.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            userFavStock
                .HasOne(f => f.FavoriteStock)
                .WithMany(f => f.ApplicationUsers)
                .HasForeignKey(f => f.FavoriteStockId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
