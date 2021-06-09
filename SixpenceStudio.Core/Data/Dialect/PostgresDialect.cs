using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Data.Dialect
{
    public class PostgresDialect : IDBDialect
    {
        public string CreateRole(string name)
        {
            return $"CREATE ROLE {name}";
        }

        public string CreateUser(string name)
        {
            return $"CREATE USER {name}";
        }

        public string DropRole(string name)
        {
            return $"DROP ROLE {name}";
        }

        public string DropUser(string name)
        {
            return $"DROP User {name}";
        }

        public string GetDataBase(string name)
        {
            return $@"
SELECT u.datname
FROM pg_catalog.pg_database u where u.datname='{name}';
";
        }

        public string GetTable(string tableName)
        {
            return $@"
SELECT * FROM pg_tables
WHERE schemaname = 'public' AND tablename = '{tableName}'";
        }

        public string QueryRole(string name)
        {
            return $@"
SELECT * FROM pg_roles
WHERE rolname = '{name}'";
        }

    }
}
