using Npgsql;
using Orderly.Users.Application.Data;
using System.Data;

namespace Orderly.Users.Infrastructure.Data;

internal sealed class SqlConnectionFactory(string connectionString)
    : ISqlConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}
