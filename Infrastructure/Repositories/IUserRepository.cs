using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task CreateAsync(User user);
}