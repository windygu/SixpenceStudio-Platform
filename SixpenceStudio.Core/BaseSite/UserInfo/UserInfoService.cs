using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System.Collections.Generic;

namespace SixpenceStudio.Core.UserInfo
{
    public class UserInfoService : EntityService<user_info>
    {
        #region 构造函数
        public UserInfoService()
        {
            this._cmd = new EntityCommand<user_info>();
        }

        public UserInfoService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<user_info>(broker);
        }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            var sql = @"
SELECT
    is_lockName,
	user_info.*
FROM
	user_info
LEFT JOIN (
    SELECT
        user_infoid,
        is_lockName
    FROM auth_user
) au ON user_info.user_infoid = au.user_infoid
";
            var customFilter = new List<string>() { "name" };
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = sql,
                    CustomFilter = customFilter,
                    OrderBy = "name, createdon",
                    ViewId = "59F908EB-A353-4205-ABE4-FA9DB27DD434",
                    Name = "所有的用户信息"
                }
            };
        }
    }
}
