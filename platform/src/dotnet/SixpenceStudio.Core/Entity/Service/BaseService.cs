using SixpenceStudio.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Entity
{
    public class BaseService
    {
        protected IPersistBroker broker;
        protected Logging.Logger logger;
    }
}
