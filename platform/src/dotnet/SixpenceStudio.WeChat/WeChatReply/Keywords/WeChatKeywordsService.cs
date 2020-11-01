#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/1 20:30:09
Description：微信关键词回复 Service
********************************************************/
#endregion

using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.WeChatReply.Keywords
{
    public class WeChatKeywordsService : EntityService<wechat_keywords>
    {
        #region 构造函数
        public WeChatKeywordsService()
        {
            this._cmd = new EntityCommand<wechat_keywords>();
        }

        public WeChatKeywordsService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<wechat_keywords>(broker);
        }
        #endregion

        /// <summary>
        /// 根据关键词获取回复消息
        /// </summary>
        /// <param name="requestMesage"></param>
        /// <returns></returns>
        public IEnumerable<wechat_keywords> GetDataList(string requestMesage)
        {
            var sql = @"
SELECT * FROM wechat_keywords
WHERE name = @name
";
            return _cmd.broker.RetrieveMultiple<wechat_keywords>(sql, new Dictionary<string, object>() { { "@name", requestMesage } });
        }
    }
}
