using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixpenceStudio.BaseSite.SysFile;
using SixpenceStudio.Platform;
using SixpenceStudio.Platform.Configs;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.Platform.Store;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixpenceStudio.Platform.Entity;

namespace SixpenceStudio.WeChat.Material
{
    public class WeChatMaterialService : EntityService<wechat_material>
    {
        #region 构造函数
        public WeChatMaterialService()
        {
            _cmd = new EntityCommand<wechat_material>();
        }

        public WeChatMaterialService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<wechat_material>(broker);
        }
        #endregion

        public override IList<EntityView<wechat_material>> GetViewList()
        {
            var sql = $"SELECT * FROM wechat_material WHERE 1=1";
            return new List<EntityView<wechat_material>>()
            {
                new EntityView<wechat_material>()
                {
                    Sql = sql,
                    CustomFilter = new List<string>() { "name" },
                    OrderBy = "createdon, name",
                    ViewId = ""
                }
            };
        }

        /// <summary>
        /// 获取素材
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
        /// 上传素材
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public string AddMaterial(MaterialType type, string fileId)
        {
            var file = new SysFileService().GetData(fileId);
            var config = ConfigFactory.GetConfig<StoreSection>();
            var stream = AssemblyUtil.GetObject<IStoreStrategy>(config?.type).GetStream(fileId);
            var media = WeChatApi.AddMaterial(type, stream, file.name, file.content_type);
            var user = _cmd.GetCurrentUser();
            var material = new wechat_material()
            {
                wechat_materialId = Guid.NewGuid().ToString(),
                media_id = media.media_id,
                url = media.url,
                sys_fileid = fileId,
                name = file.name,
                type = type.ToMaterialTypeString(),
                createdBy = user.userId,
                createdByName = user.name,
                modifiedBy = user.userId,
                modifiedByName= user.name,
                modifiedOn = DateTime.Now,
                createdOn = DateTime.Now
            };
            CreateData(material);
            return media.media_id;
        }
    }
}
