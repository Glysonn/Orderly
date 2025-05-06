using Orderly.Users.Domain.Users;

namespace Orderly.Users.Domain;

public interface IUserRepository
{
    /// <summary>
    /// Add a user to the database and return the added user.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<User?> AddAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves an existing user by their email and password
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<User?> GetByEmailAndPasswordAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}
