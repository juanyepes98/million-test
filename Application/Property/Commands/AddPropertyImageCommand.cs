using Application.Property.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Property.Commands;

public record AddPropertyImageCommand(string PropertyId, List<PropertyImageRequestDto>? Images): IRequest<bool>;

public class AddPropertyImageCommandHandler(IPropertyRepository propertyRepository) : IRequestHandler<AddPropertyImageCommand, bool>
{
    public async Task<bool> Handle(AddPropertyImageCommand request, CancellationToken cancellationToken)
    {
        // Get property
        var property = await propertyRepository.GetByIdAsync(request.PropertyId);

        // Validate if exists
        if (property is null)
        {
            throw new KeyNotFoundException($"Property with Id {request.PropertyId} not found.");
        }
        
        // Add images to property
        if (request.Images is not null)
        {
            foreach (var image in request.Images)
            {
                property.PropertyImages.Add(new PropertyImage
                {
                    File = image.File,
                    Enabled = true
                });
            }
        }
        
        // Update property
        await propertyRepository.UpdateAsync(property);
        return true;
    }
}