using Infrastructure.Repositories;
using MediatR;

namespace Application.Owner.Commands;

public record DeleteOwnerCommand(string Id) : IRequest<bool>;

public sealed class DeleteOwnerCommandHandler(IOwnerRepository repository) : IRequestHandler<DeleteOwnerCommand, bool>
{
    public async Task<bool> Handle(DeleteOwnerCommand request, CancellationToken cancellationToken)
    {
        var owner = await repository.GetByIdAsync(request.Id);
        if (owner is null) return false;

        await repository.DeleteAsync(request.Id);
        return true;
    }
}