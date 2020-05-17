using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiServices.Test.OtherTest
{
    [TestClass]
    public class CodeTest
    {
        [TestMethod]
        public void ObjectEquest()
        {
            var _t = "这是一个字符串";
            var _type = _t.GetType();
        }
    }
}
