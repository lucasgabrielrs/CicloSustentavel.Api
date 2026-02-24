// CicloSustentavel.Application/Services/ProductService.cs
using CicloSustentavel.Application.Exceptions;
using CicloSustentavel.Application.Validators;
using CicloSustentavel.Communication.Requests.Users;
using CicloSustentavel.Domain.Models;
using CicloSustentavel.Domain.Repositories;

namespace CicloSustentavel.Application.Services;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public void Validate(RegisterUserRequestJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ValidationErrorsException(errorMessages);
        }
    }

    public UserModel RegisterUser(RegisterUserRequestJson request)
    {
        Validate(request);

        var entity = new UserModel
        {
            Name = request.Name,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = request.Role
        };
        _repository.Create(entity);

        return entity;
    }

    public void LinkUserToEmpresa(Guid userId, Guid empresaId)
    {
        _repository.LinkUserToEmpresa(userId, empresaId);
    }

    public void UnlinkUserFromEmpresa(Guid userId, Guid empresaId)
    {
        _repository.UnlinkUserFromEmpresa(userId, empresaId);
    }

    public List<EmpresaModel> GetUserEmpresas(Guid userId)
    {
        return _repository.GetUserEmpresas(userId);
    }

    public List<UserModel> GetAllUsers()
    {
        return _repository.GetAll();
    }

    public UserModel? GetById(Guid id)
    {
        return _repository.GetById(id);
    }

    public UserModel? ValidateLogin(string email, string password)
    {
        var user = _repository.GetByEmail(email);

        if (user == null)
        {
            return null;
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!isPasswordValid)
        {
            return null;
        }

        return user;
    }
}