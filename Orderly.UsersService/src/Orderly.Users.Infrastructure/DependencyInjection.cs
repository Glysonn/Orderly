using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orderly.Users.Application.Data;
using Orderly.Users.Domain;
using Orderly.Users.Infrastructure.Data;
using Orderly.Users.Infrastructure.Data.TypeHandlers;
using Orderly.Users.Infrastructure.Repositories;

namespace Orderly.Users.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Extension method to add infrastructur services to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        AddPersistance(services, configuration);
        return services;
    }

    private static void AddPersistance(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        RegisterDapperTypeHandlers();
    }

    private static void RegisterDapperTypeHandlers()
    {
        SqlMapper.AddTypeHandler(new EmailTypeHandler());
        SqlMapper.AddTypeHandler(new NameTypeHandler());
        SqlMapper.AddTypeHandler(new PasswordTypeHandler());
    }
}
