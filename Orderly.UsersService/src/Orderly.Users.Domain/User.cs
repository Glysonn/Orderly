using Orderly.Users.Domain.ValueObjects;

namespace Orderly.Users.Domain;

public class User : Entity
{
    public Name Name { get; private set; }

    public Email Email { get; private set; }

    public Password Password { get; private set; }

    public Gender Gender { get; private set; }

    private User(
        Guid id,
        Name name,
        Email email,
        Password password,
        Gender? gender) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
        Gender = gender ?? Gender.Unspecified;
    }

    public static User Create(Name name,
        Email email,
        Password password,
        Gender? gender)
    {
        var user = new User(Guid.NewGuid(), name, email, password, gender);

        return user;
    }
}
