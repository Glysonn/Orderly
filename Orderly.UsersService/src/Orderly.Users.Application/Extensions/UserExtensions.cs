using Orderly.Users.Application.Models;
using Orderly.Users.Domain.Users;

namespace Orderly.Users.Application.Extensions;

internal static class UserExtensions
{
    /// <summary>
    /// Converts a <see cref="User"/> instance to an <see cref="AuthenticationResponse"/> with the user's basic data.
    /// </summary>
    /// <param name="user">The <see cref="User"/> instance to convert.</param>
    /// <returns>
    /// An <see cref="AuthenticationResponse"/> containing the user's ID, email, name, and gender.  
    /// The <c>Token</c> and <c>Success</c> properties are not set here — they should be assigned separately 
    /// depending on the authentication context.
    /// </returns>
    internal static AuthenticationResponse ToAuthenticationResponse(this User user)
        => new(user.Id,
            user.Email.Value,
            user.Name.Value,
            user.Gender);
}
