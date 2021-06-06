using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Utils;
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

        [TestMethod]
        public void TestBaseEntity()
        {
            var typeList = new List<Type>();
            AssemblyUtil.GetAssemblies("SixpenceStudio.*.dll").ForEach(item => typeList.AddRange(item.GetTypes()));
            var interfaces = typeList.Where(item => item.IsInterface && item.IsDefined(typeof(UnityRegisterAttribute), false)).ToList();
            interfaces.ForEach(item =>
            {
                var types = typeList.Where(type => !type.IsInterface && !type.IsAbstract && type.GetInterfaces().Contains(item)).ToList();
                types.ForEach(type => UnityContainerService.Register(item, type, type.Name));
            });
            var entityList = UnityContainerService.ResolveAll<IEntity>();
            Assert.IsTrue(entityList != null && entityList.Count() > 0);
        }

    }
}
