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
            //var _t = false;
            //var _type = _t.GetType();
            //Console.WriteLine(_type.FullName);
            var t = new SysRolesRequestModel() {
                Test=false,
                 role_id="123"
            };
            var s= new DbContextAbstract().GetSqlQueryString<SysRolesModel>(t);
            Console.WriteLine(s);
        }
    }
}
