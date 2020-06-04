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

        [TestMethod]
        public async Task TestDapperForSqlServer()
        {
            var _now = DateTime.Now;
            var dbContext = AutofacHelper.GetService<ISqlServerDbContext>();
            var model = new MVOnlineLogModel()
            {
                UserCode = "123456",
                UserName = "123456",
                UnitCode = "123456",
                UnitName = "123456",
                LoginDate = _now,
                CamerType = "123456",
                CamerTypeName = "123456",
                SynTime = _now,
                OrgCode = "123465",
                CameraIndexCode = "123456",
            };

            var b= await dbContext.CreateAsync(model, new string[] { "No" });
            Assert.IsTrue(b>0);
        }

        [TestMethod]
        public async Task TestUpdateDapperForSqlServer()
        {
            var _now = DateTime.Now;
            var dbContext = AutofacHelper.GetService<ISqlServerDbContext>();
            var model = new MVOnlineLogModel()
            {
                LogoutDate = _now,
                No = 5735084
            };
            var b = await dbContext.UpdateModelAsync(model, new string[] { "No"});
            Assert.IsTrue(b > 0);
        }
    }
}
