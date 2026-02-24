using CicloSustentavel.Domain.Models;
using CicloSustentavel.Domain.Repositories;
using CicloSustentavel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CicloSustentavel.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(ProductModel product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public List<ProductModel> GetAll(Guid userId)
    {
        var user = _context.Users
            .Include(u => u.Empresas)
            .FirstOrDefault(u => u.Id == userId);

        if (user == null || !user.Empresas.Any())
            return new List<ProductModel>();

        var empresaIds = user.Empresas.Select(e => e.Id).ToList();

        return _context.Products
            .Include(p => p.Empresa)
            .Where(p => empresaIds.Contains(p.EmpresaId))
            .ToList();
    }

    public ProductModel GetById(Guid id)
    {
        return _context.Products.Find(id);
    }

    public List<ProductModel> GetNearExpiration(int days)
    {
        var limitDate = DateTime.Now.AddDays(days);
        return _context.Products
            .Where(p => p.ExpirationDate <= limitDate)
            .OrderBy(p => p.ExpirationDate)
            .ToList();
    }

    public void Delete(ProductModel product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public ProductModel Update(ProductModel product)
    {
        var existingProduct = _context.Products.Find(product.Id);
        if (existingProduct == null)
            throw new Exception("Produto não encontrado.");
        existingProduct.Name = product.Name;
        existingProduct.UnitPrice = product.UnitPrice;
        existingProduct.InventoryQuantity = product.InventoryQuantity;
        existingProduct.ExpirationDate = product.ExpirationDate;
        existingProduct.Category = product.Category;
        existingProduct.UnitOfMeasurement = product.UnitOfMeasurement;
        existingProduct.Origin = product.Origin;
        existingProduct.PackagingType = product.PackagingType;
        _context.SaveChanges();
        return existingProduct;
    }
}