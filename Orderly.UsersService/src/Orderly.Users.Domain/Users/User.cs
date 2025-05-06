using Orderly.Users.Domain.Common;
using Orderly.Users.Domain.Users.ValueObjects;

namespace Orderly.Users.Domain.Users;

public class User : Entity
{
    public Name Name { get; private set; }

    public Email Email { get; private set; }

    public Password Password { get; private set; }

    public Gender Gender { get; private set; }

    private User(
        Name name,
        Email email,
        Password password,
        Gender? gender)
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
        var user = new User(name, email, password, gender);

        return user;
    }
}
