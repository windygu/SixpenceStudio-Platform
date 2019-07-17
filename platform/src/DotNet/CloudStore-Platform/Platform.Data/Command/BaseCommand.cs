using System;
using System.Collections.Generic;
using System.Text;
using Platform.Data.PersistBroker;

namespace Platform.Data.Command
{
    public abstract class BaseCommand
    {
        #region 构造函数
        protected BaseCommand() { }
        #endregion

        private IPersistBroker _broker;
        public IPersistBroker Broker
        {
            get
            {
                return _broker ?? (_broker = PersistBrokerFactory.GetPersistBroker());
             }
            protected set
            {
                this._broker = value;
            }
        }


    }
}
