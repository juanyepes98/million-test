namespace Domain.Entities;

public class Property
{
    public int IdProperty { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string CodeInternational { get; set; } = string.Empty;
    public int Year { get; set; }
    
    public int IdOwner { get; set; }
    public Owner Owner { get; set; } = null!;

    // Relationship with images
    public ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

    // Trace relationship
    public ICollection<PropertyTrace> PropertyTraces { get; set; } = new List<PropertyTrace>();
}