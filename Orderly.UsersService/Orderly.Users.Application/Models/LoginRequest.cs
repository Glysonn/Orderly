namespace Orderly.Users.Application.Models;

public sealed record LoginRequest(
    string Email,
    string Password);
