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
        public void TimeTest()
        {
            var t = DateTime.Now.ToString();
            Console.WriteLine(t);
        }
    }
}
