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

        public static CurrentUserModel GetAdmin()
        {
            var data = MemoryCacheUtil.GetCacheItem<auth_user>("Admin");
            if (data == null)
            {
                data = new AuthUserService().GetDataByCode("admin");
                MemoryCacheUtil.Set("Admin", data, 60 * 60 * 24);
            }
            return data.ToCurrentUserModel();
        }
    }
}
