using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.SysMenu
{
    public class SysMenuInitialDataProvider : IEntityInitialDataProvider
    {
        public string EntityName => "sys_menu";

        public IEnumerable<BaseEntity> GetInitialData()
        {
            var menu = new sys_menu() { Id = "DD0FCF4C-10E6-4DB5-8255-35984F5DB134", name = "系统设置", menu_Index = 30, stateCode = 1, stateCodeName = "启用" };
            return new List<sys_menu>()
            {
                menu,
                new sys_menu() { Id = "77E973D5-7EC0-4904-A43C-C6623B02D9FC", name = "菜单管理", menu_Index = 3000, router = "sysmenu", stateCode = 1, stateCodeName = "启用", parentid =  menu.Id, parentIdName = menu.name },
                new sys_menu() { Id = "C64951C4-9FE0-432D-B899-3225DA3A64FF", name = "实体", menu_Index = 3010, router = "sysEntity", stateCode = 1, stateCodeName = "启用", parentid =  menu.Id, parentIdName = menu.name },
                new sys_menu() { Id = "202FEB91-90E1-467B-9DDA-086A636DECD2", name = "作业管理", menu_Index = 3020, router = "job", stateCode = 1, stateCodeName = "启用", parentid =  menu.Id, parentIdName = menu.name },
                new sys_menu() { Id = "A0C5D935-DF51-4476-B4B0-73C3A33264DB", name = "异步管理", menu_Index = 3030, router = "async", stateCode = 1, stateCodeName = "启用", parentid =  menu.Id, parentIdName = menu.name },
                new sys_menu() { Id = "417A0A4D-44ED-43BA-B693-BA65079A8C62", name = "用户信息", menu_Index = 3040, router = "userInfo", stateCode = 1, stateCodeName = "启用", parentid =  menu.Id, parentIdName = menu.name },
                new sys_menu() { Id = "1C403F1C-F83C-4AF2-8260-AC686C5211C1", name = "选项集", menu_Index = 3050, router = "sysParamGroup", stateCode = 1, stateCodeName = "启用", parentid =  menu.Id, parentIdName = menu.name },
                new sys_menu() { Id = "35F740A3-F094-4A33-BD53-D43B489EB28E", name = "系统参数", menu_Index = 3060, router = "sysConfig", stateCode = 1, stateCodeName = "启用", parentid =  menu.Id, parentIdName = menu.name },
                new sys_menu() { Id = "9118991A-9B9E-4F1B-8858-57515AC32763", name = "角色管理", menu_Index = 3070, router = "role", stateCode = 1, stateCodeName = "启用", parentid =  menu.Id, parentIdName = menu.name },
            };
        }
    }
}
