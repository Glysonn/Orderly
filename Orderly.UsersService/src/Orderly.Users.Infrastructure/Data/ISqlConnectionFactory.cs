using System.Data;

namespace Orderly.Users.Application.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}