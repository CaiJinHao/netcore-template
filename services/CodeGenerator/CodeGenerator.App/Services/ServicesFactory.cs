using CodeGenerator.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Services
{
    public class ServicesFactory
    {
        public static TableServices CreateTableServices()
        {
            return new TableServices();
        }
    }
}
