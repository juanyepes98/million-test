using Application.Property.DTOs;
using AutoMapper;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Property.Queries;

/// <summary>
// /// Query to retrieve a property by its unique identifier.
// /// </summary>
// /// <param name="Id">The unique identifier of the property.</param>
public record GetPropertyByIdQuery(string Id) : IRequest<PropertyDto>;

/// <summary>
// /// Handler for <see cref="GetPropertyByIdQuery"/>.
// /// Retrieves a property by Id from the repository and maps it to a DTO.
// /// Throws <see cref="KeyNotFoundException"/> if the property does not exist.
// /// </summary>
public class GetPropertyByIdHandler(IPropertyRepository propertyRepository, IMapper mapper) : IRequestHandler<GetPropertyByIdQuery, PropertyDto>
{
    /// <summary>
    /// Handles the <see cref="GetPropertyByIdQuery"/>.
    /// </summary>
    /// <param name="request">The query containing the property Id.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>The <see cref="PropertyDto"/> corresponding to the requested property.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no property exists with the given Id.</exception>
    public async Task<PropertyDto> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        // Retrieve property by Id
        var property = await propertyRepository.GetByIdAsync(request.Id);
        
        // Throw exception if property not found
        if (property == null) throw new KeyNotFoundException("Property not found");

        // Map entity to DTO and return
        return mapper.Map<PropertyDto>(property);
    }
}