using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class Property
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public decimal Price { get; set; }
    public string? CodeInternational { get; set; }
    public int Year { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    // Relationship with an owner
    public string? OwnerId { get; set; }

    // Relationship with images
    public ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

    // Relationship with traces
    public ICollection<PropertyTrace> PropertyTraces { get; set; } = new List<PropertyTrace>();
}