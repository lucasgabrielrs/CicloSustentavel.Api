using CicloSustentavel.Domain.Models;

namespace CicloSustentavel.Domain.Repositories;

public interface IProductRepository
{
    void Add(ProductModel product);
    List<ProductModel> GetAll();
    List<ProductModel> GetNearExpiration(int days);
}