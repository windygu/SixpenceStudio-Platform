using SixpenceStudio.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Auth.SysRole
{
    public class SysRolePlugin : IPersistBrokerPlugin
    {
        public void Execute(Context context)
        {
            if (context.EntityName != "sys_role") return;

            var obj = context.Entity as sys_role;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    {
                        switch (obj.privilege)
                        {
                            case "all":

                                break;
                            case "group":

                                break;
                            case "user":

                                break;
                            case "guest":

                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case EntityAction.PostUpdate:
                    break;
                case EntityAction.PostDelete:
                    break;
                default:
                    break;
            }
        }
    }
}
