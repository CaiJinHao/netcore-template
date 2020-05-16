using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.App.Repository
{
    public class RepositoryFactory
    {
        public static TableRepository CreateTableRepository()
        {
            return new TableRepository();
        }

        public static ColumnRepsitory CreateColumnRepsitory()
        {
            return new ColumnRepsitory();
        }
        
    }
}
