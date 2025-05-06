using Dapper;
using Orderly.Users.Application.Data;
using Orderly.Users.Domain;
using Orderly.Users.Domain.Users;

namespace Orderly.Users.Infrastructure.Repositories;

internal sealed class UserRepository(
    ISqlConnectionFactory sqlConnectionFactory) 
    : IUserRepository
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<User?> AddAsync(
        User user, 
        CancellationToken cancellationToken = default)
    {
        const string sqlQuery = """
            INSERT INTO public.users(id, name, email, password, gender)
            VALUES (@Id, @Name, @Email, @Password, @Gender)
            """;

        using var connection = _sqlConnectionFactory.CreateConnection();
        var rowCount = await connection.ExecuteAsync(sqlQuery, user);

        if (rowCount <= 0)
        {
            return null;
        }

        return user;
    }

    public async Task<User?> GetByEmailAndPasswordAsync(
        string email, 
        string password,
        CancellationToken cancellationToken = default)
    {
        const string query = """
            SELECT * FROM public.users
            WHERE email = @Email 
                AND password = @Password;
            """;

        using var connection = _sqlConnectionFactory.CreateConnection();

        var user = await connection.QueryFirstOrDefaultAsync<User>(
            sql: query, 
            param: new
            {
                Email = email,
                Password = password
            });

        return user;
    }
}
