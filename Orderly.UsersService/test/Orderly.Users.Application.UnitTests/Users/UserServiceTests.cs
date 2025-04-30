using Moq;
using Orderly.Users.Application.Models;
using Orderly.Users.Domain;
using Orderly.Users.Domain.ValueObjects;

namespace Orderly.Users.Application.UnitTests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ReturnsAuthenticationResponse()
    {
        // Arrange
        var request = new LoginRequest("user@example.com", "password123");

        var user = User.Create(
            new Name("Name Example"),
            new Email(request.Email),
            new Password(request.Password),
            Gender.Male);

        _userRepositoryMock
            .Setup(r => r.GetByEmailAndPasswordAsync(
                request.Email, 
                request.Password, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.LoginAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(user.Email.Value, result.Email);
        Assert.False(string.IsNullOrEmpty(result.Token));
    }

    [Fact]
    public async Task LoginAsync_WithInvalidCredentials_ReturnsNull()
    {
        // Arrange
        var request = new LoginRequest("invalidmail@mail.com", "invalidpassword");

        _userRepositoryMock
            .Setup(x => x.GetByEmailAndPasswordAsync(
                request.Email,
                request.Password,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        var loggedUser = await _userService.LoginAsync(request);

        // Assert
        Assert.Null(loggedUser);
    }

    [Fact]
    public async Task RegisterAsync_WithValidData_ReturnsAuthenticationResponse()
    {
        // Arrange
        var request = new RegisterRequest("Maria", "maria@example.com", "123456", Gender.Female);

        var user = User.Create(
            new Name(request.Name),
            new Email(request.Email),
            new Password(request.Password),
            request.Gender);

        _userRepositoryMock
            .Setup(r => r.AddAsync(
                It.IsAny<User>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.RegisterAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(user.Email.Value, result.Email);
        Assert.Equal(user.Name.Value, result.Name);
        Assert.False(string.IsNullOrWhiteSpace(result.Token));
    }

    [Fact]
    public async Task RegisterAsync_WhenRepositoryReturnsNull_ReturnsNull()
    {
        // Arrange
        var request = new RegisterRequest("João", "joao@example.com", "123456", Gender.Male);

        _userRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.RegisterAsync(request);

        // Assert
        Assert.Null(result);
    }
}