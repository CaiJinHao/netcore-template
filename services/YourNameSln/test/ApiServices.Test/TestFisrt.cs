using Common.Utility.Autofac;
using DataBase.DapperForSqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using YourWebApiName.IServices.IDbServices;
using YourWebApiName.Models.DbModels;
using YourWebApiName.Models.RequestModels;

namespace ApiServices.Test
{
    [TestClass]
    public class TestFisrt:DIBase
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var roleService = AutofacHelper.GetService<ISysRolesService>();
            var dataList = await roleService.GetModelsAsync(new SysRolesRequestModel()
            {
            });
            Assert.IsTrue(dataList.ToList().Count > 0);
        }
    }
}
