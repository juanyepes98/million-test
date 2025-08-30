using Infrastructure.Repositories;
using MediatR;

namespace Application.Owner.Commands;

public record CreateOwnerCommand(string Name, string Address, string? Photo, DateTime Birthday) : IRequest<Domain.Entities.Owner>;

public class CreateOwnerCommandHandler(IOwnerRepository ownerRepository)
    : IRequestHandler<CreateOwnerCommand, Domain.Entities.Owner>
{
    public async Task<Domain.Entities.Owner> Handle(CreateOwnerCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            var owner = new Domain.Entities.Owner
            {
                Name = command.Name,
                Address = command.Address,
                Photo = command.Photo,
                Birthday = command.Birthday
            };

            await ownerRepository.AddAsync(owner);

            return owner;
        }
        catch (Exception e)
        {
            throw new Exception(e.GetBaseException().Message);
        }
    }
}