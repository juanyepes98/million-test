using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Property.DTOs;

public class PropertyRowDto: IMapFrom<Domain.Entities.Property>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public List<PropertyImage>? PropertyImages { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(Domain.Entities.Property), GetType());       
    }
}