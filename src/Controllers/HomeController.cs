using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosIdentity.src.Controllers;
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[Controller]")]
[ApiController]
public class HomeController : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Get() 
    {
        return Ok();
    }
}

