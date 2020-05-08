using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.DataService.Models
{
    public class Image
    {
        public string name { get; set; }
        public HttpPostedFile file { get; set; }
        public int? size { get; set; }
    }

    public class ImageInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
    }
}