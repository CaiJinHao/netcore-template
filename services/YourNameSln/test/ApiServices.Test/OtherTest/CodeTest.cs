using IDataBase.DbExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;

namespace ApiServices.Test.OtherTest
{
    [TestClass]
    public class CodeTest
    {
        [TestMethod]
        public void ObjectEquest()
        {
            var t = new SysRolesRequestModel() {
                Test=false,
                 role_id="123",
                  role_name="tearfsadsfsadfsa"
            };
            var s= new DbContextAbstract().GetSqlQueryString<SysRolesModel>(t);
            Console.WriteLine(s);
        }
    }
}
