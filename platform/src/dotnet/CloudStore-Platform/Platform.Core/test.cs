
using System;
using System.Collections.Generic;
using System.Text;
using Platform.Core.Entity;
using Platform.Core.Service;
using Platform.Core.Controller;
using Platform.Core.Command;
using Platform.Core.PersistBroker;
using System.Linq;

namespace Platform.Core
{
    public class TestController : EntityBaseController<TestModel, TestService>
    {
        
    }

    public class TestService : EntityService<TestModel>
    {
        public TestService()
        {
            _cmd = new EntityCommand<TestModel>();
        }

        public TestService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<TestModel>(broker);
        }

        public List<TestModel> GetNameByAge(int age)
        {
            string sql = "select * from test where age = " + age;
            return _cmd.Broker.Query<TestModel>(sql).ToList();
        }
    }

    public class TestModel : BaseEntity
    {
        public string _name;
        public string Name => _name;

        public int _age;
        public int Age => _age;

    }
}
