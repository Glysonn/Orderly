using Dapper;
using Orderly.Users.API.Middlewares;
using Orderly.Users.Application.Data;

namespace Orderly.Users.API.Extensions;

public static class ApplicationBuilderExtensions
{

    public static void UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static async Task CreateDatabaseAsync(this IApplicationBuilder builder)
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

        var connectionFactory = builder.ApplicationServices.GetRequiredService<ISqlConnectionFactory>();

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
