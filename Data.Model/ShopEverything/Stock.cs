using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model.ShopEverything
{
    using static DataValidation.DataValidation;

    public class Stock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }

        public string StockNumber { get; set; }

        public string Color { get; set; }

        public string Picture { get; set; }

        public string AddedFromUser { get; set; }

        public ICollection<UserFavoriteStocks> ApplicationUsers { get; set; } = new HashSet<UserFavoriteStocks>();
    }
}
