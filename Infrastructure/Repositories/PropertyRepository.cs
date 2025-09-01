using Domain.Entities;
using Infrastructure.Database;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class PropertyRepository(MongoDbContext context): IPropertyRepository
{
    private readonly IMongoCollection<Property> _properties = context.GetCollection<Property>("Properties");

    public async Task<Property?> GetByIdAsync(string id) =>
        await _properties.Find(p => p.Id == id).FirstOrDefaultAsync();
    
    public async Task<IEnumerable<Property>> GetAllAsync() =>
        await _properties.Find(_ => true).ToListAsync();

    public async Task<IEnumerable<Property>> GetByOwnerIdAsync(string ownerId) =>
        await _properties.Find(p => p.OwnerId == ownerId).ToListAsync();

    public async Task AddAsync(Property property) =>
        await _properties.InsertOneAsync(property);

    public async Task UpdateAsync(Property property) =>
        await _properties.ReplaceOneAsync(p => p.Id == property.Id, property);

    public async Task DeleteAsync(string id) =>
        await _properties.DeleteOneAsync(p => p.Id == id);
    
    public async Task<(IEnumerable<Property> Items, long TotalCount)> GetFilteredAsync(
        string? name, string? address, decimal? minPrice, decimal? maxPrice,
        int page, int pageSize)
    {
        var filter = Builders<Property>.Filter.Empty;

        if (!string.IsNullOrWhiteSpace(name))
            filter &= Builders<Property>.Filter.Regex(p => p.Name, new BsonRegularExpression(name, "i"));

        if (!string.IsNullOrWhiteSpace(address))
            filter &= Builders<Property>.Filter.Regex(p => p.Address, new BsonRegularExpression(address, "i"));

        if (minPrice.HasValue)
            filter &= Builders<Property>.Filter.Gte(p => p.Price, minPrice.Value);

        if (maxPrice.HasValue)
            filter &= Builders<Property>.Filter.Lte(p => p.Price, maxPrice.Value);

        var totalCount = await _properties.CountDocumentsAsync(filter);

        var items = await _properties
            .Find(filter)
            .Skip((page) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}