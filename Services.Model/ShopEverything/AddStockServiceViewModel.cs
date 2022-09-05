using Data.Model.DataValidation;
using System.ComponentModel.DataAnnotations;


namespace Services.Model.ShopEverything
{
    using static DataValidation;

    public class AddStockServiceViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }

        public string StockNumber { get; set; }

        public string Color { get; set; }

        public string Picture { get; set; }
    }
}
