using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Web;

namespace SixpenceStudio.Platform.Entity
{
    public static class EntityCommandExtension
    {
        private static string Userid
        {
            get
            {
                // var loginUser = HttpContext.Current.Request.Cookies?.Get("LoginUser")?.Values;
                //if (loginUser != null)
                //{
                //    return loginUser.Get("UserId");
                //}
                var loginUser = HttpContext.Current.Session["UserId"];
                if (loginUser != null)
                {
                    return loginUser.ToString();
                }
                return "";
            }
        }

        public static (string userId, string code, string name) GetCurrentUser<T>(this EntityCommand<T> cmd)
            where T : BaseEntity, new()
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var sql = @"
select * from auth_user where code = @code;
";
            var dataTable = broker.Query(sql, new Dictionary<string, object>() { { "@code", Userid } });
            if (dataTable.Rows.Count > 0)
            {
                var name = dataTable.Rows[0]["name"];
                var code = dataTable.Rows[0]["code"];
                var userId = dataTable.Rows[0]["user_infoid"];
                return (userId != DBNull.Value ? userId.ToString() : "", code != DBNull.Value ? code.ToString() : "", name != DBNull.Value ? name.ToString() : "");
            }
            return (null, null, null);
        }
    }
}
