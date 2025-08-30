using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IOwnerRepository
{
    Task<IEnumerable<Owner>> GetAllAsync();
    Task<Owner> GetByIdAsync(string id);
    Task AddAsync(Owner owner);
    Task UpdateAsync(Owner owner);
    Task DeleteAsync(string id);
    Task AddPropertyAsync(string ownerId, Property property);
}