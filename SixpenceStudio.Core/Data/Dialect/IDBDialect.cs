using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data.Dialect
{
    public interface IDBDialect
    {
        string CreateRole(string name);

        string DropRole(string name);

        string CreateUser(string name);
        string DropUser(string name);

        string QueryRole(string name);
        string GetDataBase(string name);
        string GetTable(string tableName);
    }
}
