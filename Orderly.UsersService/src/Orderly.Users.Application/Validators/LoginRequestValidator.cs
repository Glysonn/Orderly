using FluentValidation;
using Orderly.Users.Application.Models;

namespace Orderly.Users.Application.Validators;

internal sealed class LoginRequestValidator
    : AbstractValidator<LoginRequest>
{
    private const int PasswordMinLength = 5;

    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Email is required.")
            .EmailAddress()
                .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Password is required.")
            .MinimumLength(PasswordMinLength)
                .WithMessage($"Password must be at least {PasswordMinLength} characters long.");
    }
}
