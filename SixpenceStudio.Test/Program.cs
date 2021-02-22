using SixpenceStudio.Core.ShortUrl;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataList = ShortenUrlUtil.Generate("http://karldu.cn/blogReadonly/B9DF7640-38B3-4CA3-82F3-04D278DB68E7");
            Console.ReadKey();
        }
    }
}
