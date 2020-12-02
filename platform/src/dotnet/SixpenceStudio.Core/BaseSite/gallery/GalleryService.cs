using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
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
    }
}
