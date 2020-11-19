using SixpenceStudio.BaseSite.AuthUser;
using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.UserInfo
{
    public class UserInfoPlugin : IEntityActionPlugin
    {
        public void Execute(Context context)
        {
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    CreateAuthInfo(context.Entity, context.Broker);
                    break;
                case EntityAction.PostUpdate:
                    UpdateAuthInfo(context.Entity, context.Broker);
                    break;
                case EntityAction.PostDelete:
                    DeleteAuthInfo(context.Entity, context.Broker);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 创建用户认证信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        private void CreateAuthInfo(BaseEntity entity, IPersistBroker broker)
        {
            var authInfo = new auth_user()
            {
                auth_userId = Guid.NewGuid().ToString(),
                name = entity["name"]?.ToString(),
                code = entity["code"]?.ToString(),
                password = "96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e", // TODO 取配置文件参数
                user_infoid = entity["user_infoId"]?.ToString()
            };
            new AuthUserService(broker).CreateData(authInfo);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        private void UpdateAuthInfo(BaseEntity entity, IPersistBroker broker)
        {
            var sql = @"
SELECT * FROM auth_user
WHERE user_infoid = @id
";
            var authInfo = broker.Retrieve<auth_user>(sql, new Dictionary<string, object>() { { "@id", entity["user_infoId"]?.ToString() } });
            if (authInfo == null)
            {
                throw new SpException("用户Id不能为空");
            }
            authInfo.name = entity["name"]?.ToString();
            new AuthUserService(broker).UpdateData(authInfo);
        }

        /// <summary>
        /// 删除用户认证信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="broker"></param>
        private void DeleteAuthInfo(BaseEntity entity, IPersistBroker broker)
        {
            var sql = @"
DELETE FROM auth_user WHERE user_infoid = @id
";
            broker.Execute(sql, new Dictionary<string, object>() { { "@id", entity["user_infoId"]?.ToString() } });
        }

    }
}