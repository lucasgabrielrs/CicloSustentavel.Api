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

    public List<ProductModel> GetExpiringSoon()
    {
        return _repository.GetNearExpiration(days: 7);
    }
}