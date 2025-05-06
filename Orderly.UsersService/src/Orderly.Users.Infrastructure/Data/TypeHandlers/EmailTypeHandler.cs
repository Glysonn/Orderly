using Dapper;
using Orderly.Users.Domain.Users.ValueObjects;
using System.Data;

namespace Orderly.Users.Infrastructure.Data.TypeHandlers;

internal class EmailTypeHandler
    : SqlMapper.TypeHandler<Email>
{
    public override Email? Parse(object value) => new ((string)value);

    public override void SetValue(IDbDataParameter parameter, Email? value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value!.Value;
    }
}
