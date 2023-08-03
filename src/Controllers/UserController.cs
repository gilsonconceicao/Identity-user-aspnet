using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsuariosIdentity.src.Models.Create;
using UsuariosIdentity.src.Models.Identity;
using UsuariosIdentity.src.Services;

namespace UsuariosIdentity.src.Controllers;
[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    public ServicesUser _servicesUser;

    public UserController(ServicesUser servicesUser)
    {
        _servicesUser = servicesUser;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser(CreateUserModel newUser)
    { 
        await _servicesUser.CreateUser(newUser);
        return Ok("Usuário criado com sucesso"); 
    }

    [HttpPost("SignIn")]
    [AllowAnonymous]
    public async Task<IActionResult> SignInUser(UserSignInModel user)
    {
        var token = await _servicesUser.SignIn(user);
        return Ok(token); 
    } 
}