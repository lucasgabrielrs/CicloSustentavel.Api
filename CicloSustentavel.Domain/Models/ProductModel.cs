using CicloSustentavel.Domain.Enums;

namespace CicloSustentavel.Domain.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Category Category { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; } = string.Empty;
        public int InventoryQuantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public string Origin { get; set; } = string.Empty;
        public PackagingType PackagingType { get; set; }
    }
}