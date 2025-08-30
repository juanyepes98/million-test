using Application.Owner.DTOs;
using AutoMapper;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Owner.Queries;

public record GetOwnerByIdQuery(string Id) : IRequest<OwnerDto?>;

public class GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository, IMapper mapper) : IRequestHandler<GetOwnerByIdQuery, OwnerDto?>
{
    public async Task<OwnerDto?> Handle(GetOwnerByIdQuery request, CancellationToken cancellationToken)
    {
        var owner = await ownerRepository.GetByIdAsync(request.Id);
        if (owner == null)
            throw new KeyNotFoundException("Owner not found");

        return mapper.Map<OwnerDto>(owner);
    }
}