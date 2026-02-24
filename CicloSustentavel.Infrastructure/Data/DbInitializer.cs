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

        // Se já houver dados, não faz nada
        if (context.Users.Any() || context.Empresas.Any() || context.Products.Any()) return;

        // Seed Empresas
        var empresa1 = new EmpresaModel
        {
            Id = Guid.NewGuid(),
            NomeFantasia = "Padaria Pão Quente",
            RazaoSocial = "Padaria Pão Quente LTDA",
            Cnpj = "12.345.678/0001-90"
        };

        var empresa2 = new EmpresaModel
        {
            Id = Guid.NewGuid(),
            NomeFantasia = "Hortifruti Orgânico",
            RazaoSocial = "Hortifruti Orgânico SA",
            Cnpj = "98.765.432/0001-10"
        };

        context.Empresas.AddRange(empresa1, empresa2);
        context.SaveChanges();

        // Seed Users
        var adminUser = new UserModel
        {
            Id = Guid.NewGuid(),
            Name = "Administrador",
            Email = "admin@ciclosustentavel.com",
            Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Role = Roles.Admin
        };

        var fornecedorUser = new UserModel
        {
            Id = Guid.NewGuid(),
            Name = "João Silva",
            Email = "joao@padaria.com",
            Password = BCrypt.Net.BCrypt.HashPassword("fornecedor123"),
            Role = Roles.Fornecedor
        };

        var normalUser = new UserModel
        {
            Id = Guid.NewGuid(),
            Name = "Maria Santos",
            Email = "maria@email.com",
            Password = BCrypt.Net.BCrypt.HashPassword("user123"),
            Role = Roles.User
        };

        context.Users.AddRange(adminUser, fornecedorUser, normalUser);
        context.SaveChanges();

        // Relacionamento User-Empresa
        fornecedorUser.Empresas.Add(empresa1);
        normalUser.Empresas.Add(empresa2);
        context.SaveChanges();

        // Seed Products
        var products = new List<ProductModel>
        {
            new ProductModel
            {
                Id = Guid.NewGuid(),
                Name = "Pão Francês",
                UnitPrice = 12.00m,
                Category = Category.Padaria,
                ExpirationDate = DateTime.Now,
                InventoryQuantity = 15,
                UnitOfMeasurement = UnitOfMeasurement.Quilograma,
                Description = "Pão francês tradicional",
                Origin = "Local",
                PackagingType = PackagingType.Biodegradavel,
                EmpresaId = empresa1.Id
            },
            new ProductModel
            {
                Id = Guid.NewGuid(),
                Name = "Pão Integral",
                UnitPrice = 15.00m,
                Category = Category.Padaria,
                ExpirationDate = DateTime.Now.AddDays(2),
                InventoryQuantity = 10,
                UnitOfMeasurement = UnitOfMeasurement.Quilograma,
                Description = "Pão integral com grãos",
                Origin = "Local",
                PackagingType = PackagingType.Biodegradavel,
                EmpresaId = empresa1.Id
            },
            new ProductModel
            {
                Id = Guid.NewGuid(),
                Name = "Leite Integral",
                UnitPrice = 4.20m,
                Category = Category.Laticinios,
                ExpirationDate = DateTime.Now.AddDays(3),
                InventoryQuantity = 120,
                UnitOfMeasurement = UnitOfMeasurement.Litro,
                Description = "Leite integral pasteurizado",
                Origin = "Regional",
                PackagingType = PackagingType.Reciclavel,
                EmpresaId = empresa1.Id
            },
            new ProductModel
            {
                Id = Guid.NewGuid(),
                Name = "Tomate Orgânico",
                UnitPrice = 8.50m,
                Category = Category.FrutasVerduras,
                ExpirationDate = DateTime.Now.AddDays(10),
                InventoryQuantity = 45,
                UnitOfMeasurement = UnitOfMeasurement.Quilograma,
                Description = "Tomate orgânico cultivado localmente",
                Origin = "Local",
                PackagingType = PackagingType.Biodegradavel,
                EmpresaId = empresa2.Id
            },
            new ProductModel
            {
                Id = Guid.NewGuid(),
                Name = "Alface Orgânica",
                UnitPrice = 3.50m,
                Category = Category.FrutasVerduras,
                ExpirationDate = DateTime.Now.AddDays(5),
                InventoryQuantity = 30,
                UnitOfMeasurement = UnitOfMeasurement.Unidade,
                Description = "Alface crespa orgânica",
                Origin = "Local",
                PackagingType = PackagingType.Biodegradavel,
                EmpresaId = empresa2.Id
            },
            new ProductModel
            {
                Id = Guid.NewGuid(),
                Name = "Banana Prata",
                UnitPrice = 6.90m,
                Category = Category.FrutasVerduras,
                ExpirationDate = DateTime.Now.AddDays(7),
                InventoryQuantity = 60,
                UnitOfMeasurement = UnitOfMeasurement.Quilograma,
                Description = "Banana prata orgânica",
                Origin = "Regional",
                PackagingType = PackagingType.Biodegradavel,
                EmpresaId = empresa2.Id
            }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}