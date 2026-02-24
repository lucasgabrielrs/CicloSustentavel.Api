// CicloSustentavel.Application/Services/ProductService.cs
using CicloSustentavel.Application.Exceptions;
using CicloSustentavel.Application.Validators;
using CicloSustentavel.Communication.Requests.Products;
using CicloSustentavel.Communication.Responses.Products;
using CicloSustentavel.Domain.Models;
using CicloSustentavel.Domain.Repositories;

namespace CicloSustentavel.Application.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public void Validate(RegisterProductRequestJson request)
    {
        var validator = new RegisterProductValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }

    public void RegisterProduct(RegisterProductRequestJson request)
    {
        Validate(request);

        var entity = new ProductModel
        {
            Name = request.Name,
            UnitPrice = request.UnitPrice,
            InventoryQuantity = request.InventoryQuantity,
            ExpirationDate = request.ExpirationDate,
            Category = (Domain.Enums.Category)request.Category,
            UnitOfMeasurement = (Domain.Enums.UnitOfMeasurement)request.UnitOfMeasurement,
            Description = request.Description,
            Origin = request.Origin,
            PackagingType = (Domain.Enums.PackagingType)request.PackagingType
        };

        _repository.Add(entity);
    }

    public ResponseAllProductsJson GetAllProducts()
    {
        var products = _repository.GetAll();

        return new ResponseAllProductsJson
        {
            Products = products.Select(p => new ResponseProductJson
            {
                Id = p.Id,
                Name = p.Name,
                UnitPrice = p.UnitPrice,
                InventoryQuantity = p.InventoryQuantity,
                ExpirationDate = p.ExpirationDate,
                Category = p.Category.ToString(),
                UnitOfMeasurement = p.UnitOfMeasurement.ToString(),
                Origin = p.Origin,
                PackagingType = p.PackagingType.ToString()
            }).ToList()
        };
    }

    public ResponseProductJson GetById(Guid id)
    {
        var product = _repository.GetById(id);
        if (product == null)
            throw new ValidationErrorsException(new List<string> { "Produto não encontrado." });
        return new ResponseProductJson
        {
            Id = product.Id,
            Name = product.Name,
            UnitPrice = product.UnitPrice,
            InventoryQuantity = product.InventoryQuantity,
            ExpirationDate = product.ExpirationDate,
            Category = product.Category.ToString(),
            UnitOfMeasurement = product.UnitOfMeasurement.ToString(),
            Origin = product.Origin,
            PackagingType = product.PackagingType.ToString()
        };
    }

    public List<ProductModel> GetExpiringSoon()
    {
        return _repository.GetNearExpiration(days: 7);
    }

    public void Delete(Guid id)
    {
        var product = _repository.GetById(id);
        if (product == null)
            throw new ValidationErrorsException(new List<string> { "Produto não encontrado." });
        _repository.Delete(product);
    }

    public void Update(Guid id, RegisterProductRequestJson request)
    {
        Validate(request);
        var existingProduct = _repository.GetById(id);
        if (existingProduct == null)
            throw new ValidationErrorsException(new List<string> { "Produto não encontrado." });
        existingProduct.Name = request.Name;
        existingProduct.UnitPrice = request.UnitPrice;
        existingProduct.InventoryQuantity = request.InventoryQuantity;
        existingProduct.ExpirationDate = request.ExpirationDate;
        existingProduct.Category = (Domain.Enums.Category)request.Category;
        existingProduct.UnitOfMeasurement = (Domain.Enums.UnitOfMeasurement)request.UnitOfMeasurement;
        existingProduct.Description = request.Description;
        existingProduct.Origin = request.Origin;
        existingProduct.PackagingType = (Domain.Enums.PackagingType)request.PackagingType;
        _repository.Update(existingProduct);
    }
}