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
using SixpenceStudio.Core.Store;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Data;

namespace SixpenceStudio.WeChat.Material
{
    public class WeChatMaterialPlugin : IPersistBrokerPlugin
    {
        public void Execute(PluginContext context)
        {
            var entity = context.Entity as wechat_material;
            switch (context.Action)
            {
                case EntityAction.PreCreate:
                case EntityAction.PreUpdate:
                    // 如果素材未上传到系统，则根据url请求图片保存
                    if (string.IsNullOrEmpty(entity.sys_fileid))
                    {
                        var result = HttpUtil.DownloadImage(entity.url, out var contentType);
                        var stream = StreamUtil.BytesToStream(result);
                        var hash_code = SHAUtil.GetFileSHA1(stream);
                        var config = ConfigFactory.GetConfig<StoreSection>();
                        UnityContainerService.Resolve<IStoreStrategy>(config?.type).Upload(stream, entity.name, out var filePath);
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
                        entity.sys_fileid = id;
                        entity.local_url = $"/api/SysFile/Download?objectId={id}";
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
