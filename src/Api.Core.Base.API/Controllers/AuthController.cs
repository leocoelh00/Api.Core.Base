using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Core.Base.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("credentials")]
    public IActionResult GetCredentials() => Ok(new
    {
        username = configuration["BasicAuth:Username"],
        password = configuration["BasicAuth:Password"]
    });
}
