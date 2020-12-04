using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Pixabay;
using SixpenceStudio.Core.Store;
using SixpenceStudio.Core.SysFile;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.gallery
{
    public class GalleryService : EntityService<gallery>
    {
        #region 构造函数
        public GalleryService()
        {
            _cmd = new EntityCommand<gallery>();
        }

        public GalleryService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<gallery>(Broker);
        }
        #endregion

        private string DownloadImage(string url, string objectid)
        {
            var result = HttpUtil.DownloadImage(url, out var contentType);
            var stream = StreamUtil.BytesToStream(result);
            var hash_code = SHAUtil.GetFileSHA1(stream);
            var config = ConfigFactory.GetConfig<StoreSection>();
            var fileName = url.Substring(url.LastIndexOf("/") + 1);
            UnityContainerService.Resolve<IStoreStrategy>(config?.type).Upload(stream, fileName, out var filePath);

            var data = new sys_file()
            {
                sys_fileId = Guid.NewGuid().ToString(),
                name = fileName,
                hash_code = hash_code,
                file_path = filePath,
                file_type = "gallery",
                content_type = contentType,
                objectId = objectid
            };
            return Broker.Create(data);
        }

        public string UploadImage(Pixabay.ImageModel image)
        {
            return Broker.ExecuteTransaction(() =>
            {
                var data = new gallery()
                {
                    Id = Guid.NewGuid().ToString(),
                    tags = image.tags
                };
                data.previewid = DownloadImage(image.previewURL, data.Id);
                data.imageid = DownloadImage(image.largeImageURL, data.Id);
                data.preview_url = $"api/SysFile/Download?objectid={data.previewid}";
                data.image_url = $"api/SysFile/Download?objectid={data.imageid}";
                base.CreateData(data);
                return data.previewid;
            });
        }
    }
}
