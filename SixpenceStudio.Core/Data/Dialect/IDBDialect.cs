using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data.Dialect
{
    public interface IDBDialect
    {
        string CreateTable(string name);
        string CreateRole(string name);
        string DropRole(string name);
        string CreateUser(string name);
        string DropUser(string name);
        string QueryRole(string name);
        string GetDataBase(string name);
        string GetTable(string tableName);
        string GetAddColumnSql(string tableName, List<Attr> columns);
        string GetDropColumnSql(string tableName, List<Attr> columns);
        string CreateTemporaryTable(string tableName, out string newTableName);
    }
}
