namespace Application.Property.DTOs;

public class PropertyTraceRequestDto
{
    public DateTime DateSale { get; set; }
    public string? Name { get; set; }
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
}