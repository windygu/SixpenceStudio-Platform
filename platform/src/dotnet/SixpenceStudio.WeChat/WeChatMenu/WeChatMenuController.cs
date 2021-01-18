using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.WeChat.WeChatMenu
{
    [RequestAuthorize]
    public class WeChatMenuController : BaseController
    {
        [HttpPost]
        public void CreateMenu(WeChatMenuModel menu)
        {
            WeChatMenuService.CreateMenu(menu);
        }

        [HttpGet]
        public IEnumerable<WeChatMenuModel> GetMenu()
        {
            return WeChatMenuService.GetMenu();
        }

        [HttpGet]
        public void DeleteMenu()
        {
            WeChatMenuService.DeleteMenu();
        }
    }
}
