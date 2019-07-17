using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Data.PersistBroker
{
    public static class PersistBrokerFactory
    {
        public static IPersistBroker GetPersistBroker()
        {
            return new PersistBroker();
        }
    }
}
