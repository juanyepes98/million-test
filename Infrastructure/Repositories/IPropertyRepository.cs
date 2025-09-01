using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IPropertyRepository
{
    Task<Property?> GetByIdAsync(string id);
    Task<IEnumerable<Property>> GetAllAsync();
    Task<IEnumerable<Property>> GetByOwnerIdAsync(string ownerId);
    Task AddAsync(Property property);
    Task UpdateAsync(Property property);
    Task DeleteAsync(string id);
    Task<(IEnumerable<Property> Items, long TotalCount)> GetFilteredAsync(
        string? name, string? address, decimal? minPrice, decimal? maxPrice,
        int page, int pageSize);
}