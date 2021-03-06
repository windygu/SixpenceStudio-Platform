using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Core.UserInfo
{
    public class UserInfoController : EntityBaseController<user_info, UserInfoService>
    {
        [HttpGet, AllowAnonymous]
        public override user_info GetData(string id)
        {
            return base.GetData(id);
        }
    }
}
