#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/8 16:32:01
Description：
********************************************************/
#endregion

using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.WeChatReply.Focus
{
    public class WeChatFocusReplyPlugin : IEntityActionPlugin
    {
        public void Execute(Context context)
        {
            switch (context.Action)
            {
                case Platform.Command.Action.PreCreate:
                case Platform.Command.Action.PreUpdate:
                    var entity = context.Entity;
                    if (entity.GetAttributeValue("checked") == null)
                    {
                        context.Entity["checked"] = 0;
                    }
                    if (entity.GetAttributeValue("wechat") == null)
                    {
                        context.Entity["wechat"] = ConfigFactory.GetConfig<WeChatSection>().appid;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
