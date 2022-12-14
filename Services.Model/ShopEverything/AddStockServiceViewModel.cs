using Data.Model.DataValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Services.Model.ShopEverything
{
    using static DataValidation;

    public class AddStockServiceViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }

        public string StockNumber { get; set; }

        public string Color { get; set; }

        public IFormFile Picture { get; set; }
    }
}
