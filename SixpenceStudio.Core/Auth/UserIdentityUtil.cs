using SixpenceStudio.Core.AuthUser;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth
{
    /// <summary>
    /// 用户身份认证帮助类
    /// </summary>
    public class UserIdentityUtil
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public static string UserId
        {
            get
            {
                return ApplicationContext.Current.User.Id;
            }
        }

        /// <summary>
        /// 设置当前线程用户
        /// </summary>
        /// <param name="user"></param>
        public static void SetCurrentUser(CurrentUserModel user)
        {
            if (user != null)
            {
                ApplicationContext.Current.User = user;
            }
        }

        /// <summary>
        /// 获取当前线程用户
        /// </summary>
        /// <returns></returns>
        public static CurrentUserModel GetCurrentUser()
        {
            return ApplicationContext.Current.User;
        }
        
        /// <summary>
        /// 获取当前线程用户Id
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserId()
        {
            return ApplicationContext.Current?.User?.Id;
        }

        private static readonly object lockAnonymousObject = new Object();
        /// <summary>
        /// 获取匿名用户对象
        /// </summary>
        /// <returns></returns>
        public static CurrentUserModel GetAnonymous()
        {
            var data = MemoryCacheUtil.GetCacheItem<auth_user>("auth_user_anonymous");
            if (data == null)
            {
                lock (lockAnonymousObject)
                {
                    if (data == null)
                    {
                        data = new AuthUserService().GetDataByCode("Anonymous");
                        MemoryCacheUtil.Set("auth_user_anonymous", data, 3600 * 24);
                    }
                }
            }
            return data.ToCurrentUserModel();
        }

        private static readonly object lockAdminObject = new Object();
        /// <summary>
        /// 获取管理员对象
        /// </summary>
        /// <returns></returns>
        public static CurrentUserModel GetAdmin()
        {
            var data = MemoryCacheUtil.GetCacheItem<auth_user>("auth_user_admin");
            if (data == null)
            {
                lock (lockAdminObject)
                {
                    if (data == null)
                    {
                        data = new AuthUserService().GetDataByCode("admin");
                        MemoryCacheUtil.Set("auth_user_admin", data, 3600 * 24);
                    }
                }
            }
            return data.ToCurrentUserModel();
        }
    }
}
