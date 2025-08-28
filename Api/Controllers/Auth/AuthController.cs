using Infrastructure.Authentication;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserRepository userRepository, JwtTokenGenerator tokenGenerator)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await userRepository.GetByUsernameAsync(username);
        if (user == null || user.PasswordHash != password) // 🔒 usar hashing real (BCrypt/Argon2)
            return Unauthorized();

        var token = tokenGenerator.GenerateToken(user);
        return Ok(new { Token = token });
    }
}