using Infrastructure.Repositories;
using MediatR;

namespace Application.Property.Commands;

public record DeletePropertyCommand(string Id): IRequest<bool>;

public class DeletePropertyCommandHandler(IPropertyRepository propertyRepository) : IRequestHandler<DeletePropertyCommand, bool>
{
    public async Task<bool> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = await propertyRepository.GetByIdAsync(request.Id);

        if (property == null)
            throw new KeyNotFoundException($"Property with Id {request.Id} not found.");

        await propertyRepository.DeleteAsync(request.Id);
        return true;
    }
}
