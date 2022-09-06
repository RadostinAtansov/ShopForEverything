using Data.Model.DataValidation;
using System.ComponentModel.DataAnnotations;

namespace Services.Model.ShopEverything
{
    using static DataValidation;

    public class ShowAllStockServiceViewModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }

        public string StockNumber { get; set; }
    }
}
