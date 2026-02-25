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
    public async Task<IActionResult> Create([FromBody] RegisterUserRequestJson request)
    {
        try
        {
            var response = _service.RegisterUser(request);
            return Created(string.Empty, response);
        }
        catch (ValidationErrorsException ex)
        {
            return BadRequest(new { errors = ex.ErrorMessages });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetAllUsers();
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
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _service.GetById(id);
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
    public async Task<IActionResult> LinkToEmpresa(Guid userId, Guid empresaId)
    {
        try
        {
            await _service.LinkUserToEmpresa(userId, empresaId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{userId}/empresas/{empresaId}")]
    public async Task<IActionResult> UnlinkFromEmpresa(Guid userId, Guid empresaId)
    {
        try
        {
            await _service.UnlinkUserFromEmpresa(userId, empresaId);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{userId}/empresas")]
    public async Task<IActionResult> GetUserEmpresas(Guid userId)
    {
        var empresas = await _service.GetUserEmpresas(userId);
        return Ok(empresas);
    }
}