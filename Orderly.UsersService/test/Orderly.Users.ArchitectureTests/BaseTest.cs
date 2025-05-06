using Orderly.Users.API.Middlewares;
using Orderly.Users.Application.Services;
using Orderly.Users.Domain.Common;
using Orderly.Users.Infrastructure.Data;
using System.Reflection;

namespace Orderly.Users.ArchitectureTests;

public class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;

    protected static readonly Assembly ApplicationAssembly = typeof(UserService).Assembly;

    protected static readonly Assembly InfrastructureAssembly = typeof(SqlConnectionFactory).Assembly;

    protected static readonly Assembly PresentationAssembly = typeof(ExceptionHandlingMiddleware).Assembly;
}
