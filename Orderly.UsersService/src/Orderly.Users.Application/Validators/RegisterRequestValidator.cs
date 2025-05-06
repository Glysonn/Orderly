using FluentValidation;
using Orderly.Users.Application.Models;

namespace Orderly.Users.Application.Validators;

internal sealed class RegisterRequestValidator
    : AbstractValidator<RegisterRequest>
{
    private const int NameMinLength = 5;
    private const int NameMaxLength = 255;

    private const int PasswordMinLength = 5;

    public RegisterRequestValidator()
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

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Name is required.")
            .MinimumLength(NameMinLength)
                .WithMessage($"Name must be at least {NameMinLength} characters long.")
            .MaximumLength(NameMaxLength)
                .WithMessage($"Name cannot exceed {NameMaxLength} characters.");

        RuleFor(x => x.Gender)
            .IsInEnum()
                .WithMessage("Invalid gender value.");
    }
}
