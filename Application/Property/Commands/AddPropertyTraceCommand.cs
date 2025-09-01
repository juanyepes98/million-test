using Application.Property.DTOs;
using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Property.Commands;

public record AddPropertyTraceCommand(string PropertyId, List<PropertyTraceRequestDto>? Traces): IRequest<bool>;

public class AddPropertyTraceCommandHandler(IPropertyRepository propertyRepository)
    : IRequestHandler<AddPropertyTraceCommand, bool>
{
    public async Task<bool> Handle(AddPropertyTraceCommand request, CancellationToken cancellationToken)
    {
        // Get property
        var property = await propertyRepository.GetByIdAsync(request.PropertyId);

        // Validate if exists
        if (property is null)
        {
            throw new KeyNotFoundException($"Property with Id {request.PropertyId} not found.");
        }
        
        // Add traces
        if (request.Traces is not null)
        {
            foreach (var trace in request.Traces)
            {
                property.PropertyTraces.Add(new PropertyTrace
                {
                    DateSale = trace.DateSale,
                    Name = trace.Name,
                    Value = trace.Value,
                    Tax = trace.Tax,
                });
            }
        }
        
        // Update property
        await propertyRepository.UpdateAsync(property);
        return true;
    }
}

