using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Orderly.Users.Application.Services;

namespace Orderly.Users.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Extension method to add application services to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
}
