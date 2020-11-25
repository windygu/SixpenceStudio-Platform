using SixpenceStudio.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.Core.AuthUser
{
    public static class IPersistBrokerSecurityExtension
    {
        private static string Userid = HttpContext.Current?.Session?["UserId"]?.ToString();

        public static void SetCurrentUser(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                Userid = userId;
            }
        }

        public static CurrentUserModel GetCurrentUser(this IPersistBroker broker)
        {
            var data = broker.Retrieve<auth_user>("select * from auth_user where code = @code;", new Dictionary<string, object>() { { "@code", Userid } });
            return data?.ToCurrentUserModel();
        }

        public static CurrentUserModel GetAdmin(this IPersistBroker broker)
        {
            var data = broker.Retrieve<auth_user>("select * from auth_user where code = @code", new Dictionary<string, object>() { { "@code", "admin" } });
            return data?.ToCurrentUserModel();
        }

        public static CurrentUserModel ToCurrentUserModel(this auth_user entity)
        {
            return new CurrentUserModel()
            {
                Id = entity?.Id,
                Code = entity?.code,
                Name = entity?.name
            };
        }
    }
    public class CurrentUserModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}