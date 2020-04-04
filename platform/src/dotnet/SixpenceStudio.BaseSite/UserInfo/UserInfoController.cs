using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.BaseSite.UserInfo
{
    [RequestAuthorize]
    public class UserInfoController : EntityController<user_info, UserInfoService>
    {

    }
}
