namespace Application.Owner.DTOs;

public class UpdateOwnerDto
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Photo { get; set; } = null!;
    public DateTime Birthday { get; set; }
}