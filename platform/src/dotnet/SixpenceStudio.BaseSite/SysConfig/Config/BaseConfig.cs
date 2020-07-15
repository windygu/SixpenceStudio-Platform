using SixpenceStudio.BaseSite.SysConfig;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;

namespace SixpenceStudio.BaseSite.SysConfig.Config
{
    /// <summary>
    /// Base of Config Class
    /// </summary>
    public abstract class BaseConfig
    {
        /// <summary>
        /// Config name
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Config unique code
        /// </summary>
        public abstract string Code { get; }

        /// <summary>
        /// Config default value
        /// </summary>
        public abstract object DefaultValue { get; }

        /// <summary>
        /// the method of getting value( if you have not set it, you will get default value which you set before )
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            var sql = @"
select * from sys_config where code = @code;
";
            var broker = new PersistBroker();
            var data = broker.Retrieve<sys_config>(sql, new Dictionary<string, object>() { { "@code", Code } });
            if (data == null)
            {
                var model = new sys_config()
                {
                    name = Name,
                    code = Code,
                    value = DefaultValue?.ToString()
                };
                new SysConfigService().CreateData(model);
                return DefaultValue;
            }
            return data.value;
        }
    }
}