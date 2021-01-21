using SixpenceStudio.Core.Pixabay;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Core.gallery
{
    public class GalleryController : EntityBaseController<gallery, GalleryService>
    {
        [HttpGet]
        public ImagesModel GetImages(string searchValue, int pageIndex, int pageSize)
        {
            return new PixabayService().GetImages(searchValue, pageIndex, pageSize);
        }

        [HttpPost]
        public (string previewid, string imageid) UploadImage(ImageModel image)
        {
            return new GalleryService().UploadImage(image);
        }
    }
}
