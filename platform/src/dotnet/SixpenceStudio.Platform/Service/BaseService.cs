using SixpenceStudio.Platform.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Service
{
    public class BaseService
    {
        protected IPersistBroker broker;
        protected Logging.Logger logger;
    }
}
