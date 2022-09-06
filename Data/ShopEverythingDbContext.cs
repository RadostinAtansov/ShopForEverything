using Data.Model.ShopEverything;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data
{
    public class ShopEverythingDbContext : IdentityDbContext<IdentityUser>
    {

        public ShopEverythingDbContext(DbContextOptions<ShopEverythingDbContext> options) : base(options) {}

        public DbSet<Stock> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(DataSettings.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
