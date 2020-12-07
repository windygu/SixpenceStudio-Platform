using SixpenceStudio.Core.AuthUser;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth
{
    public class UserIdentityUtil
    {
        public static string UserId
        {
            get
            {
                return ApplicationContext.Current.User.Id;
            }
        }

        public static void SetCurrentUser(CurrentUserModel user)
        {
            if (user != null)
            {
                ApplicationContext.Current.User = user;
            }
        }

        public static CurrentUserModel GetCurrentUser()
        {
            return ApplicationContext.Current.User;
        }

        public static CurrentUserModel GetAnonymous()
        {
            var data = MemoryCacheUtil.GetCacheItem<auth_user>("auth_user_anonymous");
            if (data == null)
            {
                data = new AuthUserService().GetDataByCode("Anonymous");
                MemoryCacheUtil.Set("auth_user_anonymous", data, 3600 * 24);
            }
            return data.ToCurrentUserModel();
        }

        public static CurrentUserModel GetAdmin()
        {
            var data = MemoryCacheUtil.GetCacheItem<auth_user>("auth_user_admin");
            if (data == null)
            {
                data = new AuthUserService().GetDataByCode("admin");
                MemoryCacheUtil.Set("auth_user_admin", data, 3600 * 24);
            }
            return data.ToCurrentUserModel();
        }
    }
}
