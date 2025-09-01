using Domain.Entities;
using Infrastructure.Database;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class OwnerRepository(MongoDbContext context) : IOwnerRepository
{
    private readonly IMongoCollection<Owner> _owners = context.GetCollection<Owner>("Owners");

    public async Task<Owner> GetByIdAsync(string id)
    {
        return await _owners.Find(o => o.Id == id).FirstOrDefaultAsync();
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
        await _owners.ReplaceOneAsync(o => o.Id == owner.Id, owner);
    }

    public async Task DeleteAsync(string id)
    {
        await _owners.DeleteOneAsync(o => o.Id == id);
    }
}