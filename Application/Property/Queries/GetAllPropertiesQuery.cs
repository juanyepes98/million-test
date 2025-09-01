using Application.Common.DTOs;
using Application.Property.DTOs;
using AutoMapper;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Property.Queries;

/// <summary>
// /// Query to retrieve a paginated list of properties with optional filtering.
// /// </summary>
// /// <param name="Skip">Number of items to skip for pagination (default: 1).</param>
// /// <param name="Take">Number of items to take for pagination (default: 10).</param>
// /// <param name="Name">Optional filter for property name.</param>
// /// <param name="Address">Optional filter for property address.</param>
// /// <param name="MinPrice">Optional filter for minimum price.</param>
// /// <param name="MaxPrice">Optional filter for maximum price.</param>
public record GetAllPropertiesQuery(int Skip = 1, 
    int Take = 10, 
    string? Name = null, 
    string? Address = null, 
    decimal? MinPrice = null, 
    decimal? MaxPrice = null): IRequest<PagedResultDto<PropertyRowDto>>;

/// <summary>
// /// Handler for <see cref="GetAllPropertiesQuery"/>.
// /// Retrieves properties from the repository applying filters and pagination, then maps them to DTOs.
// /// </summary>
public class GetAllPropertiesHandler(IPropertyRepository propertyRepository, IMapper mapper)
    : IRequestHandler<GetAllPropertiesQuery, PagedResultDto<PropertyRowDto>>
{
    // <summary>
    /// Handles the <see cref="GetAllPropertiesQuery"/>.
    /// </summary>
    /// <param name="request">The query containing pagination and filter parameters.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A <see cref="PagedResultDto{PropertyRowDto}"/> containing the filtered properties and total count.</returns>
    public async Task<PagedResultDto<PropertyRowDto>> Handle(GetAllPropertiesQuery request,
        CancellationToken cancellationToken)
    {
        // Retrieve filtered and paginated items along with the total count
        var (items, totalCount) = await propertyRepository.GetFilteredAsync(
            request.Name, request.Address, request.MinPrice, request.MaxPrice,
            request.Skip, request.Take);
        
        // Map entities to DTOs
        var dtos = mapper.Map<IEnumerable<PropertyRowDto>>(items);

        // Return the paginated result
        return new PagedResultDto<PropertyRowDto>
        {
            Items = dtos,
            TotalCount = totalCount,
            Page = request.Skip,
            PageSize = request.Take
        };
    }
}