using Orderly.Users.API.Middlewares;
using Orderly.Users.Application.Data;
using Orderly.Users.Infrastructure.Data;

namespace Orderly.Users.API.Extensions;

public static class ApplicationBuilderExtensions
{

    public static void UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static async Task CreateDatabaseAsync(this IApplicationBuilder builder)
    {
        var connectionFactory = builder.ApplicationServices.GetRequiredService<ISqlConnectionFactory>();
        await DatabaseInitializer.CreateDatabaseAsync(connectionFactory);
    }

}
