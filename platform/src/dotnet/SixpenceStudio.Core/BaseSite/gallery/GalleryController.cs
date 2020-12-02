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
        public ImagesModel GetImages(string searchValue)
        {
            return PixabayApi.GetImages(searchValue);
        }
    }
}
