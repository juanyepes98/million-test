using Infrastructure.Repositories;
using MediatR;

namespace Application.Property.Commands;

public sealed record CreatePropertyCommand(string OwnerId, string Name, string Address, int Year, decimal Price, string CodeInternational) : IRequest<string?>;

public sealed class CreatePropertyCommandHandler(
    IOwnerRepository ownerRepository, 
    IPropertyRepository propertyRepository)
    : IRequestHandler<CreatePropertyCommand, string?>
{
    public async Task<string?> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
    {
        // Validate that the Owner exists
        var owner = await ownerRepository.GetByIdAsync(request.OwnerId);
        if (owner is null)
            throw new KeyNotFoundException($"Owner with Id {request.OwnerId} not found.");

        // Create the property
        var property = new Domain.Entities.Property
        {
            OwnerId = owner.Id,
            Name = request.Name,
            Address = request.Address,
            Year = request.Year,
            Price = request.Price,
            CodeInternational = request.CodeInternational,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Save property
        await propertyRepository.AddAsync(property);

        return property.Id;
    }
}