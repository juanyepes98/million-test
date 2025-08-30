using Application.Common.Mappings;
using AutoMapper;

namespace Application.Owner.DTOs;

public class OwnerRowDto: IMapFrom<Domain.Entities.Owner>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(Domain.Entities.Owner), GetType());
    }
}