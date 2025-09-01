using Api.Common.Helpers;
using Application.Property.Commands;
using Application.Property.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Property;

/// <summary>
/// Controller to manage Property entities.
/// Provides endpoints to create, retrieve, update, and delete properties.
/// Uses CQRS pattern with MediatR and centralized exception handling via ControllerHelper.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PropertyController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Creates a new property.
    /// </summary>
    /// <param name="cmd">Command containing property creation data.</param>
    /// <returns>API response containing the created property Id.</returns>
    [HttpPost("")]
    public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyCommand cmd)
    {
        return await ControllerHelper.HandleRequestAsync(async () => await mediator.Send(cmd));
    }
    
    /// <summary>
    /// Retrieves a paginated list of properties with optional filtering by name, address, and price range.
    /// Accessible anonymously.
    /// </summary>
    /// <param name="skip">Number of records to skip for pagination.</param>
    /// <param name="take">Number of records to take for pagination.</param>
    /// <param name="name">Optional filter by property name.</param>
    /// <param name="address">Optional filter by property address.</param>
    /// <param name="minPrice">Optional minimum price filter.</param>
    /// <param name="maxPrice">Optional maximum price filter.</param>
    /// <returns>API response containing a paginated list of properties.</returns>
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int skip, [FromQuery] int take, [FromQuery] string? name,
        [FromQuery] string? address, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
    {
        return await ControllerHelper.HandleRequestAsync(async () =>
            await mediator.Send(new GetAllPropertiesQuery(skip, take, name, address, minPrice, maxPrice)));
    }

    /// <summary>
    /// Retrieves a property by its unique Id.
    /// Accessible anonymously.
    /// </summary>
    /// <param name="id">The unique identifier of the property.</param>
    /// <returns>API response containing the property DTO.</returns>
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return await ControllerHelper.HandleRequestAsync(async () => await mediator.Send(new GetPropertyByIdQuery(id)));
    }

    /// <summary>
    /// Deletes a property by its unique Id.
    /// </summary>
    /// <param name="id">The unique identifier of the property to delete.</param>
    /// <returns>API response indicating success or failure.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(string id)
    {
        return await ControllerHelper.HandleRequestAsync(async () =>
            await mediator.Send(new DeletePropertyCommand(id)));
    }
    
    /// <summary>
    /// Adds images to an existing property.
    /// </summary>
    /// <param name="cmd">Command containing property Id and image data.</param>
    /// <returns>API response indicating success or failure.</returns>
    [HttpPut("add-property-images")]
    public async Task<IActionResult> AddPropertyImages([FromBody] AddPropertyImageCommand cmd)
    {
        return await ControllerHelper.HandleRequestAsync(async () => await mediator.Send(cmd));
    }
    
    /// <summary>
    /// Adds trace entries to an existing property.
    /// </summary>
    /// <param name="cmd">Command containing property Id and trace data.</param>
    /// <returns>API response indicating success or failure.</returns>
    [HttpPut("add-property-traces")]
    public async Task<IActionResult> AddPropertyTraces([FromBody] AddPropertyTraceCommand cmd)
    {
        return await ControllerHelper.HandleRequestAsync(async () => await mediator.Send(cmd));
    }
}