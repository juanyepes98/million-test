using Application.Owner.DTOs;
using AutoMapper;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Owner.Commands;

public record UpdateOwnerCommand(
    string Id,
    string Name,
    string Address,
    string Photo,
    DateTime Birthday
) : IRequest<OwnerDto>;

public sealed class UpdateOwnerCommandHandler(IOwnerRepository repository, IMapper mapper)
    : IRequestHandler<UpdateOwnerCommand, OwnerDto>
{
    public async Task<OwnerDto> Handle(UpdateOwnerCommand request, CancellationToken cancellationToken)
    {
        var owner = await repository.GetByIdAsync(request.Id)
                    ?? throw new KeyNotFoundException("Owner not found");

        owner.Name = request.Name;
        owner.Address = request.Address;
        owner.Photo = request.Photo;
        owner.Birthday = request.Birthday;  

        await repository.UpdateAsync(owner);

        return mapper.Map<OwnerDto>(owner);
    }
}