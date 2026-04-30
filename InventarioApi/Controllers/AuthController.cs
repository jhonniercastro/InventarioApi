using InventarioApi.Models;
using InventarioCCL.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventarioCCL.API.Controllers;
[Route("auth")]
[ApiController]
public class AuthController : Controller
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserModel user)
    {
        var token = _authService.Authenticate(user);

        if (token == null)
            return Unauthorized(new { message = "Credenciales incorrectas" });

        return Ok(new { token });
    }
}
