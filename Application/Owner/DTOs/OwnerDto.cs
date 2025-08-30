using Application.Common.Mappings;
using Application.Owner.Commands;
using AutoMapper;

namespace Application.Owner.DTOs;

public class OwnerDto: IMapFrom<Domain.Entities.Owner>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Photo { get; set; }
    public DateTime Birthday { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(Domain.Entities.Owner), GetType());
    }
}