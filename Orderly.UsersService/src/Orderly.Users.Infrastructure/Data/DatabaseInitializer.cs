using Dapper;
using Orderly.Users.Application.Data;

namespace Orderly.Users.Infrastructure.Data;

public static class DatabaseInitializer
{
    public static async Task CreateDatabaseAsync(ISqlConnectionFactory connectionFactory)
    {
        const string createUsersTable = @"
        CREATE TABLE IF NOT EXISTS public.users (
            id UUID PRIMARY KEY,
            name TEXT NOT NULL,
            email TEXT NOT NULL UNIQUE,
            password TEXT NOT NULL,
            gender INT NOT NULL
        );";

        const int maxRetries = 5;
        var retryDelay = TimeSpan.FromSeconds(2);
        int attempt = 0;
        bool success = false;

        do
        {
            try
            {
                using var connection = connectionFactory.CreateConnection();
                await connection.ExecuteAsync(createUsersTable);
                success = true;
            }
            catch (Exception)
            {
                attempt++;
                if (attempt >= maxRetries)
                    throw;

                Console.WriteLine($"[DB Initialization] Attempt {attempt} failed. Retrying in {retryDelay.TotalSeconds} seconds...");
                await Task.Delay(retryDelay);
            }
        } while (!success);
    }
}
