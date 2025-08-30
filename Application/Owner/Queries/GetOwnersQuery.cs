using Application.Owner.DTOs;
using AutoMapper;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Owner.Queries;

public class GetAllOwnersQuery : IRequest<List<OwnerRowDto>> { }

public class GetAllOwnersQueryHandler(IOwnerRepository repository, IMapper mapper)
    : IRequestHandler<GetAllOwnersQuery, List<OwnerRowDto>>
{
    public async Task<List<OwnerRowDto>> Handle(GetAllOwnersQuery request, CancellationToken cancellationToken)
    {
        var owners = await repository.GetAllAsync();
        return mapper.Map<List<OwnerRowDto>>(owners);
    }
}