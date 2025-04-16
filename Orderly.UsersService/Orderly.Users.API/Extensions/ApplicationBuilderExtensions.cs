using Orderly.Users.API.Middlewares;

namespace Orderly.Users.API.Extensions;

public static class ApplicationBuilderExtensions
{

    public static void UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
