using Orderly.Users.Application.Models;

namespace Orderly.Users.Application;

/// <summary>
/// Contract for users service that contains use cases for users.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Handle user login.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns>A Task of <see cref="AuthenticationResponse"/> type that contains status of login. </returns>
    Task<AuthenticationResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Handle user registration.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// returns>A Task of <see cref="AuthenticationResponse"/> type that contains status of user registration. </returns>
    Task<AuthenticationResponse?> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
}
