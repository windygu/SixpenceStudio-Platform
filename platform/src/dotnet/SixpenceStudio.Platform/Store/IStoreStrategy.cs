using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Store
{
    /// <summary>
    /// 存储策略接口
    /// </summary>
    public interface IStoreStrategy
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        void Upload(Stream stream, string fileName);

        void DownLoad();
    }
}
