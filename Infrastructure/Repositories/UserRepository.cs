using Domain.Entities;
using Infrastructure.Database;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class UserRepository(MongoDbContext context) : IUserRepository
{
    private readonly IMongoCollection<User> _users = context.GetCollection<User>("Users");

    public async Task<User?> GetByUsernameAsync(string username)
        => await _users.Find(u => u.Username == username).FirstOrDefaultAsync();

    public async Task CreateAsync(User user)
        => await _users.InsertOneAsync(user);
}