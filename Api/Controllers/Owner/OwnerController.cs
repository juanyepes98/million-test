using Api.Common;
using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Owner.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Owner;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OwnerController(IMediator mediator): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOwnerCommand command)
    {
        try
        {
            var res = await mediator.Send(command);
            return Ok(ApiResponse<Domain.Entities.Owner>.SuccessResponse(res));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<LoginResponseDto>.ErrorResponse(e.Message));
        }
    }
}