using Dapper;
using Orderly.Users.Domain.ValueObjects;
using System.Data;

namespace Orderly.Users.Infrastructure.Data.TypeHandlers;

internal class PasswordTypeHandler 
    : SqlMapper.TypeHandler<Password>
{
    public override Password Parse(object value) => new((string)value);

    public override void SetValue(IDbDataParameter parameter, Password? value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value!.Value;
    }
}