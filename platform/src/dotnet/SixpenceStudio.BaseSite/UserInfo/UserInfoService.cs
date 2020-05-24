using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace SixpenceStudio.BaseSite.UserInfo
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

        public override IList<EntityView<user_info>> GetViewList()
        {
            var sql = @"
SELECT
	*
FROM
	user_info
";
            return new List<EntityView<user_info>>()
            {
                new EntityView<user_info>()
                {
                    Sql = sql,
                    OrderBy = "name, createdon",
                    ViewId = "59F908EB-A353-4205-ABE4-FA9DB27DD434",
                    Name = "所有的用户信息"
                }
            };
        }
    }
}
