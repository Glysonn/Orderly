using Microsoft.AspNetCore.Mvc;
using Orderly.Users.Application.Models;
using Orderly.Users.Application.Services;

namespace Orderly.Users.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            return BadRequest("Invalid registration data");
        }

        var response = await _userService.RegisterAsync(request, cancellationToken);
        if (response is null || !response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            return BadRequest("Invalid registration data");
        }

        var response = await _userService.LoginAsync(request, cancellationToken);

        if (response is null || !response.Success)
        {
            return Unauthorized(response);
        }

        return Ok(response);
    }
}
