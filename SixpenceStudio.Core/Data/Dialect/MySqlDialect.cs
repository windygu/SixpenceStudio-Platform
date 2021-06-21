using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data.Dialect
{
    public class MySqlDialect : IDBDialect
    {
        public string CreateRole(string name)
        {
            throw new NotImplementedException();
        }

        public string CreateTable(string name)
        {
            throw new NotImplementedException();
        }

        public string CreateTemporaryTable(string tableName, out string newTableName)
        {
            throw new NotImplementedException();
        }

        public string CreateUser(string name)
        {
            throw new NotImplementedException();
        }

        public string DropRole(string name)
        {
            throw new NotImplementedException();
        }

        public string DropUser(string name)
        {
            throw new NotImplementedException();
        }

        public string GetAddColumnSql(string tableName, List<Attr> columns)
        {
            throw new NotImplementedException();
        }

        public string GetDataBase(string name)
        {
            throw new NotImplementedException();
        }

        public string GetDropColumnSql(string tableName, List<Attr> columns)
        {
            throw new NotImplementedException();
        }

        public string GetTable(string tableName)
        {
            throw new NotImplementedException();
        }

        public string QueryRole(string name)
        {
            throw new NotImplementedException();
        }
    }
}
