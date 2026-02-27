// CicloSustentavel.Application/Services/ProductService.cs
using CicloSustentavel.Application.Validators;
using CicloSustentavel.Communication.Requests.Users;
using CicloSustentavel.Communication.Responses.Users;
using CicloSustentavel.Domain.Models;
using CicloSustentavel.Domain.Repositories;
using CicloSustentavel.Domain.Security.Cryptography;
using CicloSustentavel.Domain.Security.Tokens;
using CicloSustentavel.Exception.ExceptionsBase;

namespace CicloSustentavel.Application.Services;

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordEncrypt _passwordEncrypt;
    private readonly IAccesTokenGenerator _tokenGenerator;

    public UserService(IUserRepository repository, IPasswordEncrypt passwordEncrypt,IAccesTokenGenerator tokenGenerator)
    {
        _repository = repository;
        _passwordEncrypt = passwordEncrypt;
        _tokenGenerator = tokenGenerator;
    }

    public void Validate(RegisterUserRequestJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }

    public ResponseRegisteredUserJson RegisterUser(RegisterUserRequestJson request)
    {
        Validate(request);

        var entity = new UserModel
        {
            Name = request.Name,
            Email = request.Email,
            Password = _passwordEncrypt.Encrypt(request.Password),
            Role = request.Role
        };
        _repository.Create(entity);

        return new ResponseRegisteredUserJson
        {
            Name = entity.Name,
            Token = _tokenGenerator.GenerateToken(entity),
            EmpresaIds = new List<Guid>()
        };
    }

    public async Task<ResponseRegisteredUserJson> Login(LoginUserRequest request)
    {
        var user = await _repository.GetByEmail(request.Email);

        if (user == null)
        {
            throw new System.Exception();
        }

        var passwordMatch = _passwordEncrypt.Verify(request.Password, user.Password);

        if(!passwordMatch)
        {
            throw new System.Exception();
        }

        return new ResponseRegisteredUserJson {
            Name = request.Email,
            Token = _tokenGenerator.GenerateToken(new UserModel { Email = request.Email }),
            EmpresaIds = new List<Guid>()
        };
    }

    public async Task LinkUserToEmpresa(Guid userId, Guid empresaId)
    {
        await _repository.LinkUserToEmpresa(userId, empresaId);
    }

    public async Task UnlinkUserFromEmpresa(Guid userId, Guid empresaId)
    {
        await _repository.UnlinkUserFromEmpresa(userId, empresaId);
    }

    public async Task<List<EmpresaModel>> GetUserEmpresas(Guid userId)
    {
        return await _repository.GetUserEmpresas(userId);
    }

    public async Task<List<UserModel>> GetAllUsers()
    {
        return await _repository.GetAll();
    }

    public async Task<UserModel?> GetById(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task<UserModel?> ValidateLogin(string email, string password)
    {
        var user = await _repository.GetByEmail(email);

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