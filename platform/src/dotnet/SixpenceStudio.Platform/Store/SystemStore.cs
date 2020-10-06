using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Store
{
    public class SystemStore : IStoreStrategy
    {
        public void DownLoad()
        {
            throw new NotImplementedException();
        }

        public void Upload(Stream stream, string fileName)
        {
            FileUtil.SaveFile(stream, fileName);
        }
    }
}
