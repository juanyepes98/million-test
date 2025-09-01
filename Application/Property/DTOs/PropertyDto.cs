using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Property.DTOs;

public class PropertyDto: IMapFrom<Domain.Entities.Property>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string? CodeInternal { get; set; }
    public string? Address { get; set; }
    public string? OwnerId { get; set; }
    public List<PropertyImage>? PropertyImages { get; set; }
    public List<PropertyTrace>? PropertyTraces { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(Domain.Entities.Property), GetType());
    }
}