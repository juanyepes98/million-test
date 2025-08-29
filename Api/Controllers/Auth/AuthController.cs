using Api.Common;
using Application.Auth.Commands;
using Application.Auth.DTOs;
using Infrastructure.Authentication;
using Infrastructure.Repositories;
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
        try
        {
            var res = await mediator.Send(cmd);
            return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(res));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<LoginResponseDto>.ErrorResponse(e.Message));
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand cmd)
    {
        try
        {
            var success = await mediator.Send(cmd);
            
            if (!success)
                return BadRequest(ApiResponse<LoginResponseDto>.ErrorResponse("The user already exists."));
            
            return Ok(ApiResponse<string>.SuccessResponse("User successfully registered"));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<LoginResponseDto>.ErrorResponse(e.Message));
        }
    }
}