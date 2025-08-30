using Api.Common;
using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Owner.Commands;
using Application.Owner.DTOs;
using Application.Owner.Queries;
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
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateOwnerDto dto)
    {
        try
        {
            var command = new UpdateOwnerCommand(
                Id: id,
                Name: dto.Name,
                Address: dto.Address,
                Photo: dto.Photo,
                Birthday: dto.Birthday
            );
            
            if (id != command.Id)
                return BadRequest(ApiResponse<OwnerDto>.ErrorResponse("Id mismatch"));

            var owner = await mediator.Send(command);
            return Ok(ApiResponse<OwnerDto>.SuccessResponse(owner));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<LoginResponseDto>.ErrorResponse(e.Message));
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var success = await mediator.Send(new DeleteOwnerCommand(id));
            if (!success) return NotFound(ApiResponse<bool>.ErrorResponse("Owner not found"));

            return Ok(ApiResponse<bool>.SuccessResponse(success));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<LoginResponseDto>.ErrorResponse(e.Message));
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var res = await mediator.Send(new GetAllOwnersQuery());
            return Ok(ApiResponse<List<OwnerRowDto>>.SuccessResponse(res));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<LoginResponseDto>.ErrorResponse(e.Message));
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        try
        {
            var res = await mediator.Send(new GetOwnerByIdQuery(id));

            if (res == null)
                return NotFound(ApiResponse<LoginResponseDto>.ErrorResponse("Owner not found."));

            return Ok(ApiResponse<OwnerDto>.SuccessResponse(res));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<LoginResponseDto>.ErrorResponse(e.Message));
        }
    }
}