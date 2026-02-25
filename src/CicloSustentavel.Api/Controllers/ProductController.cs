using CicloSustentavel.Application.Exceptions;
using CicloSustentavel.Application.Services;
using CicloSustentavel.Communication.Requests.Products;
using CicloSustentavel.Communication.Responses.Products;
using Microsoft.AspNetCore.Mvc;

namespace CicloSustentavel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _service;

    public ProductsController(ProductService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Create([FromBody] RegisterProductRequestJson request)
    {
        try
        {
            _service.RegisterProduct(request);
            return Created(string.Empty, null);
        }
        catch (ValidationErrorsException ex)
        {
            return BadRequest(new { errors = ex.ErrorMessages });
        }
    }

    [HttpGet]
    [Route("listAll")]
    [ProducesResponseType(typeof(ResponseAllProductsJson), StatusCodes.Status200OK)]
    public IActionResult GetAll([FromHeader(Name = "X-User-Id")] Guid userId)
    {
        if (userId == Guid.Empty)
            return BadRequest(new { message = "UserId é obrigatório no header X-User-Id" });

        var response = _service.GetAllProducts(userId);

        if (response.Products.Any())
            return Ok(response);

        return NoContent();
    }

    [HttpGet]
    [Route("expiringSoon")]
    [ProducesResponseType(typeof(ResponseProductJson), StatusCodes.Status200OK)]
    public IActionResult GetExpiringSoon()
    {
        var response = _service.GetExpiringSoon();

        if (response.Count > 0)
            return Ok(response);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _service.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(Guid id, [FromBody] RegisterProductRequestJson request)
    {
        try
        {
            _service.Update(id, request);
            return NoContent();
        }
        catch (ValidationErrorsException ex)
        {
            return BadRequest(new { errors = ex.ErrorMessages });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}