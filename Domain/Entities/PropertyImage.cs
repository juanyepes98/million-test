using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class PropertyImage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? IdPropertyImage { get; set; }  
    public string File { get; set; } = string.Empty;
    public bool Enabled { get; set; }
    
    public int IdProperty { get; set; }       
    public Property Property { get; set; } = null!;
}