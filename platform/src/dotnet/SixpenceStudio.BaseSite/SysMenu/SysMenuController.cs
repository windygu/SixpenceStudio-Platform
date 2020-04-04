using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.BaseSite.SysMenu
{
    public class SysMenuController : EntityController<sys_menu, SysMenuService>
    {
        [HttpGet]
        public IList<sys_menu> GetFirstMenu()
        {
            return new SysMenuService().GetFirstMenu();
        }
    }
}