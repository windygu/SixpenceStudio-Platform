using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.Platform.Service;
using SixpenceStudio.Platform.Utils;
using SixpenceStudio.WeChat.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.Material
{
    public class WeChatMaterialService : BaseService
    {
        public WeChatMaterialService()
        {
            broker = PersistBrokerFactory.GetPersistBroker();
            logger = LogFactory.GetLogger("wechat");
        }

        /// <summary>
        /// 获取图文素材
        /// </summary>
        /// <param name="type">素材的类型，图片（image）、视频（video）、语音 （voice）、图文（news）</param>
        /// <param name="pageIndex">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="pageSize">返回素材的数量，取值在1到20之间</param>
        /// <returns></returns>
        public WeChatOtherMaterial GetMaterial(string type, int pageIndex, int pageSize)
        {
            var result = WeChatApi.BatchGetMaterial(type, pageIndex, pageSize);
            var materialList = JsonConvert.DeserializeObject<WeChatOtherMaterial>(result);
            if (materialList == null || materialList.item == null || materialList.item.Count <= 0)
            {
                return materialList;
            }

            materialList.item.ForEach(item =>
            {
                var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                item.UpdateTime = start.AddMilliseconds(item.update_time * 1000).ToLocalTime().ToString("yyyy-MM-dd HH:mm");
            });
            return materialList;
        }

        /// <summary>
        /// 获取图文素材
        /// </summary>
        /// <param name="pageIndex">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="pageSize">返回素材的数量，取值在1到20之间</param>
        /// <returns></returns>
        public WeChatNewsMaterial GetNewsMaterial(int pageIndex, int pageSize)
        {
            var result = WeChatApi.BatchGetMaterial("news", pageIndex, pageSize);
            var materialList = JsonConvert.DeserializeObject<WeChatNewsMaterial>(result);
            if (materialList == null || materialList.item == null || materialList.item.Count <= 0)
            {
                return materialList;
            }

            materialList.item.ForEach(item =>
            {
                var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                item.UpdateTime = start.AddMilliseconds(item.update_time * 1000).ToLocalTime().ToString("yyyy-MM-dd HH:mm");
            });
            return materialList;
        }

    }
}
