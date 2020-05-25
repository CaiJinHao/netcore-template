using IDataBase;
using IDataBase.ISql;
using System.Data;

namespace DataBase.DapperForSqlServer
{
    public interface ISqlServerDbContext : ISqlDbContext,IDbContext<IDbConnection>
    {
    }
}
