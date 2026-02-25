using CicloSustentavel.Domain.Models;

namespace CicloSustentavel.Domain.Repositories;

public interface IProductRepository
{
    void Add(ProductModel product);
    List<ProductModel> GetAll(Guid userId);
    List<ProductModel> GetNearExpiration(int days);
    ProductModel GetById(Guid id);
    void Delete(ProductModel product);
    ProductModel Update(ProductModel product);
}