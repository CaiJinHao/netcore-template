using DataBase.IDataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataBase.DapperForMySql
{
    public interface IMySqlDbContext: ISqlDbContext,IDbContext<IDbConnection>
    {
    }
}
