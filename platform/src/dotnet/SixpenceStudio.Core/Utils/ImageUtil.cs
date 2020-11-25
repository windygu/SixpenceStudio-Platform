using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Utils
{
    public static class ImageUtil
    {
        public static Image GetImage(Stream stream)
        {
            return Image.FromStream(stream);
        }
    }
}
