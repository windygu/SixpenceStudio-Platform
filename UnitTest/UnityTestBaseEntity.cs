using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixpenceStudio.Core.SysEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class UnityTestBaseEntity
    {
        [TestMethod]
        public void TestGetAttrs()
        {
            var entity = new sys_entity();
            var attrs = entity.GetAttrs();
            Assert.IsTrue(attrs != null && attrs.Count() > 0);
        }
    }
}
