#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/21 0:05:34
Description：素材Plugin
********************************************************/
#endregion

using SixpenceStudio.Core.SysFile;
using SixpenceStudio.Core;
using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Store;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixpenceStudio.Core.IoC;

namespace SixpenceStudio.WeChat.Material
{
    public class WeChatMaterialPlugin : IEntityActionPlugin
    {
        public void Execute(Context context)
        {
            var entity = context.Entity;
            switch (context.Action)
            {
                case EntityAction.PreCreate:
                case EntityAction.PreUpdate:
                    // 如果素材未上传到系统，则根据url请求图片保存
                    if (string.IsNullOrEmpty(entity.GetAttributeValue<string>("sys_fileid")))
                    {
                        var result = HttpUtil.DownloadImage(entity.GetAttributeValue<string>("url"));
                        var stream = StreamUtil.BytesToStream(result);
                        var hash_code = SHAUtil.GetFileSHA1(stream);
                        var image = ImageUtil.GetImage(stream);
                        var config = ConfigFactory.GetConfig<StoreSection>();
                        UnityContainerService.Resolve<IStoreStrategy>(config?.type).Upload(stream, entity.name, out var filePath);
                        var contentType = entity.GetAttributeValue<string>("type")+ " / " + entity.GetAttributeValue<string>("url").GetSubString("wx_fmt =");
                        var sysImage = new sys_file()
                        {
                            sys_fileId = Guid.NewGuid().ToString(),
                            name = entity.name,
                            hash_code = hash_code,
                            file_path = filePath,
                            file_type = "wechat_material",
                            content_type = contentType,
                            objectId = entity.Id
                        };
                        var id = new SysFileService(context.Broker).CreateData(sysImage);
                        entity.SetAttributeValue("sys_fileid", id);
                        entity.SetAttributeValue("width", image?.Width);
                        entity.SetAttributeValue("height", image?.Height);
                        entity.SetAttributeValue("local_url", $"/api/SysFile/Download?objectId={id}");
                    }
                    break;
                case EntityAction.PreDelete:
                    WeChatApi.DeleteMaterial(entity.GetAttributeValue<string>("media_id"));
                    break;
                default:
                    break;
            }
        }
    }
}
