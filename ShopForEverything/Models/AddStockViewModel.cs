using Data.Model.DataValidation;
using System.ComponentModel.DataAnnotations;

namespace ShopForEverything.Models
{
    using static DataValidation;

    public class AddStockViewModel
    {

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
    }
}
