using CicloSustentavel.Application.Exceptions;
using CicloSustentavel.Application.Services;
using CicloSustentavel.Communication.Requests.Users;
using CicloSustentavel.Communication.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace CicloSustentavel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Create([FromBody] RegisterUserRequestJson request)
    {
        try
        {
            var user = _service.RegisterUser(request);
            var response = new ResponseUserJson
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                EmpresaIds = user.Empresas.Select(e => e.Id).ToList()
            };
            return Created(string.Empty, response);
        }
        catch (ValidationErrorsException ex)
        {
            return BadRequest(new { errors = ex.ErrorMessages });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUserRequest request)
    {
        var user = _service.ValidateLogin(request.Email, request.Password);

        if (user == null)
        {
            return Unauthorized(new { message = "E-mail ou senha inválidos." });
        }

        var response = new ResponseUserJson
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            EmpresaIds = user.Empresas.Select(e => e.Id).ToList()
        };

        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _service.GetAllUsers();
        var response = users.Select(user => new ResponseUserJson
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            EmpresaIds = user.Empresas.Select(e => e.Id).ToList()
        }).ToList();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var user = _service.GetById(id);
        if (user == null)
        {
            return NotFound();
        }
        var response = new ResponseUserJson
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            EmpresaIds = user.Empresas.Select(e => e.Id).ToList()
        };
        return Ok(response);
    }

    [HttpPost("{userId}/empresas/{empresaId}")]
    public IActionResult LinkToEmpresa(Guid userId, Guid empresaId)
    {
        try
        {
            _service.LinkUserToEmpresa(userId, empresaId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{userId}/empresas/{empresaId}")]
    public IActionResult UnlinkFromEmpresa(Guid userId, Guid empresaId)
    {
        try
        {
            _service.UnlinkUserFromEmpresa(userId, empresaId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{userId}/empresas")]
    public IActionResult GetUserEmpresas(Guid userId)
    {
        var empresas = _service.GetUserEmpresas(userId);
        return Ok(empresas);
    }
}