using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Web;

namespace SixpenceStudio.Platform.Command
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

        public static (string userId, string name) GetCurrentUser<T>(this EntityCommand<T> cmd)
            where T : BaseEntity, new()
        {
            var broker = new PersistBroker();
            var sql = @"
select * from auth_user where code = @code;
";
            var dataTable = broker.Query(sql, new Dictionary<string, object>() { { "@code", Userid } });
            if (dataTable.Rows.Count > 0)
            {
                var name = dataTable.Rows[0]["name"];
                return (Userid, name != DBNull.Value ? name.ToString() : "");
            }
            return (null, null);
        }
    }
}
