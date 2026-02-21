using CicloSustentavel.Domain.Models;
using CicloSustentavel.Domain.Repositories;
using CicloSustentavel.Infrastructure.Data;

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

    public List<ProductModel> GetAll()
    {
        return _context.Products.ToList();
    }

    public List<ProductModel> GetNearExpiration(int days)
    {
        var limitDate = DateTime.Now.AddDays(days);
        return _context.Products
            .Where(p => p.ExpirationDate <= limitDate)
            .OrderBy(p => p.ExpirationDate)
            .ToList();
    }
}