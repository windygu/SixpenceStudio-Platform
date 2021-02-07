using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Core.SysMenu
{
    public class SysMenuController : EntityBaseController<sys_menu, SysMenuService>
    {
        [HttpGet]
        public IList<sys_menu> GetFirstMenu()
        {
            return new SysMenuService().GetFirstMenu();
        }
    }
}