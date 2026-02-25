namespace CicloSustentavel.Communication.Responses.Products;

public class ResponseProductJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int InventoryQuantity { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Category { get; set; } = string.Empty;
    public string UnitOfMeasurement { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string PackagingType { get; set; } = string.Empty;
}