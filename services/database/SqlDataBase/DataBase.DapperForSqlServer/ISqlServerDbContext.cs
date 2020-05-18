using DataBase.IDataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataBase.DapperForSqlServer
{
    public interface ISqlServerDbContext : ISqlDbContext,IDbContext<IDbConnection>
    {
    }
}
