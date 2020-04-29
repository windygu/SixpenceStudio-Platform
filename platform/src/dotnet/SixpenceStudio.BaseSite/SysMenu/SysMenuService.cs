using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysMenu
{
    public class SysMenuService : EntityService<sys_menu>
    {
        #region 构造函数
        public SysMenuService()
        {
            this._cmd = new EntityCommand<sys_menu>();
        }

        public SysMenuService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<sys_menu>(broker);
        }
        #endregion

        public override IList<sys_menu> GetDataList(IList<SearchCondition> searchList)
        {
            var data = base.GetDataList(searchList).ToList();
            var firstMenu = data.Where(e => string.IsNullOrEmpty(e.parentid)).ToList();
            firstMenu.ForEach(item =>
            {
                item.ChildMenus = new List<sys_menu>();
                data.ForEach(item2 =>
                {
                    if (item2.parentid == item.Id)
                    {
                        item.ChildMenus.Add(item2);
                    }
                });
                item.ChildMenus = item.ChildMenus.OrderBy(e => e.menu_Index).ToList();
            });
            firstMenu = firstMenu.OrderBy(e => e.menu_Index).ToList();
            return firstMenu;
        }

        public IList<sys_menu> GetFirstMenu()
        {
            var sql = @"
SELECT * FROM sys_menu
WHERE parentid IS NULL
ORDER BY menu_index
";
            var data = _cmd.broker.RetrieveMultiple<sys_menu>(sql);
            return data;
        }
    }
}