using DataBase.IDataBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataBase.MySqlFromSqlSugar
{
    public interface IMySqlSqlSugarDbContext: ISqlDbContext, IDbContext<SqlSugarClient>
    {

    }
}
