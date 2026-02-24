using CicloSustentavel.Domain.Enums;
using CicloSustentavel.Domain.Models;
using BCrypt.Net;

namespace CicloSustentavel.Infrastructure.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        // Garante que o banco foi criado
        context.Database.EnsureCreated();

        // Se já houver produtos, não faz nada
        if (context.Products.Any()) return;

        var products = new List<ProductModel>
        {
            new ProductModel
            {
                Name = "Pão Francês Seed",
                UnitPrice = 12.00m,
                Category = Category.Padaria,
                ExpirationDate = DateTime.Now, // Vence HOJE para testar o alerta vermelho
                InventoryQuantity = 15,
                UnitOfMeasurement = UnitOfMeasurement.Quilograma,
                Description = "Teste",
                Origin = "Local",
                PackagingType = PackagingType.Biodegradavel
            },
            new ProductModel
            {
                Name = "Leite Integral Seed",
                UnitPrice = 4.20m,
                Category = Category.Laticinios,
                ExpirationDate = DateTime.Now.AddDays(3), // Alerta amarelo
                InventoryQuantity = 120,
                UnitOfMeasurement = UnitOfMeasurement.Litro,
                Description = "Teste",
                Origin = "Local",
                PackagingType = PackagingType.Biodegradavel
            },
            new ProductModel
            {
                Name = "Tomate Orgânico Seed",
                UnitPrice = 8.50m,
                Category = Category.FrutasVerduras,
                ExpirationDate = DateTime.Now.AddDays(10), // Alerta verde
                InventoryQuantity = 45,
                UnitOfMeasurement = UnitOfMeasurement.Quilograma,
                Description = "Teste",
                Origin = "Local",
                PackagingType = PackagingType.Biodegradavel
            }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}