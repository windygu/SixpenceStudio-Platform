using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixpenceStudio.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class UnitTestPersistBroker
    {
        [TestMethod]
        public void TestPersistBroker()
        {
            var broker = PersistBrokerFactory.GetPersistBroker("Host=127.0.0.1;Port=5432;User ID=postgres;Password=123123;Database=postgres;");
            Assert.IsNotNull(broker.DbClient);
        }
    }
}
