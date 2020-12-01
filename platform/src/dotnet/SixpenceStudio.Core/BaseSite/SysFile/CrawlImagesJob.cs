using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Job;
using SixpenceStudio.Core.Store;
using SixpenceStudio.Core.Utils;
using SixpenceStudio.Core.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace SixpenceStudio.Core.SysFile
{
    public class CrawlImagesJob : JobBase
    {
        public override string Name => "爬取图片资源";

        public override string Description => "爬取图片资源（50张）";

        public override string CronExperssion => "";

        public override void Execute(IPersistBroker broker)
        {
            for (int i = 0; i < 50; i++)
            {
                Thread.Sleep(new Random().Next(1, 1000));
                var imgUrl = JsonConvert.DeserializeObject<ImageModel>(HttpUtil.Get("https://api.ixiaowai.cn/api/api.php?return=json"))?.imgurl;
                var result = HttpUtil.DownloadImage(imgUrl, out var contentType);
                var stream = StreamUtil.BytesToStream(result);
                var hash_code = SHAUtil.GetFileSHA1(stream);
                var config = ConfigFactory.GetConfig<StoreSection>();
                var fileName = imgUrl.Substring(imgUrl.LastIndexOf("/") + 1);
                UnityContainerService.Resolve<IStoreStrategy>(config?.type).Upload(stream, fileName, out var filePath);

                var data = new sys_file()
                {
                    sys_fileId = Guid.NewGuid().ToString(),
                    name = fileName,
                    hash_code = hash_code,
                    file_path = filePath,
                    file_type = "CrawlImagesJob",
                    content_type = contentType,
                    objectId = "985BBCC6-105E-4161-8173-139276F91677"
                };
                broker.Create(data);
            }
        }
    }

    public class ImageModel
    {
        public string code { get; set; }
        public string imgurl { get; set; }
        public string width { get; set; }
        public string height { get; set; }
    }
}
