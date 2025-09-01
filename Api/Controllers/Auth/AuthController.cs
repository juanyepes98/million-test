using Api.Common;
using Api.Common.Helpers;
using Application.Auth.Commands;
using Application.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand cmd)
    {
        return await ControllerHelper.HandleRequestAsync(
            async () => await mediator.Send(cmd));
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand cmd)
    {
        return await ControllerHelper.HandleRequestAsync(
            async () => await mediator.Send(cmd));
    }
}