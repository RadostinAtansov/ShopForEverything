namespace Data.Model.DataValidation
{
    public static class DataValidation
    {
        
        public const int NameMinLength = 2;
        public const int NameMaxLength = 100;

        public const int DescriptionMinLength = 5;
        public const int DescriptionMaxLength = 500;

        public const int PriceMinLength = int.MinValue;
        public const int PriceMaxLength = int.MaxValue;

        public const int SizeMinLength = 20;
        public const int SizeMaxLength = 80;

        public const int StockNumberMinLength = int.MinValue;
        public const int StockNumberMaxLength = int.MaxValue;

        
    }
}
