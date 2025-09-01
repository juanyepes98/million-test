using Domain.Entities;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Auth.Commands;

public record RegisterUserCommand(string Username, string Password) : IRequest<string?>;

public class RegisterUserCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, string?>
{
    public async Task<string?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check if it already exists
        var existing = await userRepository.GetByUsernameAsync(request.Username);
        if (existing != null)
            throw new Exception("Username already exists.");

        // Hashing the password
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHash
        };

        await userRepository.CreateAsync(user);
        return user.Id;
    }
}
