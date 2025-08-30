using Domain.Entities;
using Infrastructure.Database;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class OwnerRepository(MongoDbContext context) : IOwnerRepository
{
    private readonly IMongoCollection<Owner> _owners = context.GetCollection<Owner>("Owners");

    public async Task<Owner> GetByIdAsync(string id)
    {
        return await _owners.Find(o => o.IdOwner == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Owner>> GetAllAsync()
    {
        return await _owners.Find(_ => true).ToListAsync();
    }

    public async Task AddAsync(Owner owner)
    {
        await _owners.InsertOneAsync(owner);
    }

    public async Task UpdateAsync(Owner owner)
    {
        await _owners.ReplaceOneAsync(o => o.IdOwner == owner.IdOwner, owner);
    }

    public async Task DeleteAsync(string id)
    {
        await _owners.DeleteOneAsync(o => o.IdOwner == id);
    }
    
    public async Task AddPropertyAsync(string ownerId, Property property)
    {
        var filter = Builders<Owner>.Filter.Eq(o => o.IdOwner, ownerId);
        var update = Builders<Owner>.Update.Push(o => o.Properties, property);

        await _owners.UpdateOneAsync(filter, update);
    }
}