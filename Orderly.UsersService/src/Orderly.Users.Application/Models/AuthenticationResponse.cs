using Orderly.Users.Domain;

namespace Orderly.Users.Application.Models;

public sealed record AuthenticationResponse(
    Guid UserId,
    string? Email,
    string? Name,
    Gender? Gender,
    string? Token = null,
    bool Success = false
);

