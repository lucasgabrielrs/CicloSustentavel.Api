using CicloSustentavel.Application.Services;
using CicloSustentavel.Communication.Requests.Users;
using CicloSustentavel.Communication.Responses;
using CicloSustentavel.Communication.Responses.Users;
using CicloSustentavel.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace CicloSustentavel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [FromServices] UserService userService,
            [FromBody] LoginUserRequest request)
        {
            try
            {
                var response = await userService.Login(request);
                return Ok(response);
            }
            catch (InvalidLoginException ex)
            {
                return Unauthorized(new ResponseErrorJson(ex.Message));
            }
        }
    }
}
