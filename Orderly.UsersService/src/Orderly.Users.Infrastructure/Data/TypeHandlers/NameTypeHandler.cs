using Dapper;
using Orderly.Users.Domain.Users.ValueObjects;
using System.Data;

namespace Orderly.Users.Infrastructure.Data.TypeHandlers;

internal class NameTypeHandler 
    : SqlMapper.TypeHandler<Name>
{
    public override Name Parse(object value) => new((string)value);

    public override void SetValue(IDbDataParameter parameter, Name? value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value!.Value;
    }
}