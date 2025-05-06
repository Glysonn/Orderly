using Orderly.Users.Application.Extensions;
using Orderly.Users.Application.Models;
using Orderly.Users.Domain;
using Orderly.Users.Domain.Users;
using Orderly.Users.Domain.Users.ValueObjects;

namespace Orderly.Users.Application.Services;

public class UserService(IUserRepository userRepository)
    : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<AuthenticationResponse?> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAndPasswordAsync(
            request.Email,
            request.Password,
            cancellationToken);

        if (user is null)
        {
            return null;
        }

        var response = user.ToAuthenticationResponse() with
        {
            Success = true,
            Token = "token"
        };

        return response;
    }

    public async Task<AuthenticationResponse?> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        var newUser = User.Create(new Name(request.Name),
            new Email(request.Email),
            new Password(request.Password),
            request.Gender);

        var user = await _userRepository.AddAsync(newUser, cancellationToken);

        if (user is null)
            return null;

        var response = user.ToAuthenticationResponse() with
        {
            Success = true,
            Token = "token"
        };

        return response;
    }
}
