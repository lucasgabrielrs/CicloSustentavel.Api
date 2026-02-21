namespace CicloSustentavel.Communication.Requests.Products
{
    public class RegisterProductRequestJson
    {
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int InventoryQuantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Category { get; set; }
        public int UnitOfMeasurement { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public int PackagingType { get; set; }
    }
}
