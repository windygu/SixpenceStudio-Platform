using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.Core.ShortUrl
{
    public class ShortUrlService : EntityService<short_url_log>
    {
        #region 构造函数
        public ShortUrlService()
        {
            this._cmd = new EntityCommand<short_url_log>();
        }

        public ShortUrlService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<short_url_log>(broker);
        }
        #endregion

        public override string CreateData(short_url_log t)
        {
            if (string.IsNullOrEmpty(t.long_url))
            {
                return "";
            }
            return Broker.ExecuteTransaction(() =>
            {
                var data = Broker.Retrieve<short_url_log>("select * from short_url_log where long_url = @long_url", new Dictionary<string, object>() { { "@long_url", t.long_url } });
                if (data != null)
                {
                    return data.Id;
                }
                var baseUrl = ConfigFactory.GetConfig<ShortUrlSection>("url");
                t.short_key = ShortenUrlUtil.Generate(t.long_url);
                t.short_url = baseUrl + t.short_key;
                return base.CreateData(t);
            });
        }

        /// <summary>
        /// 获取短链接
        /// </summary>
        /// <param name="shortid"></param>
        /// <returns></returns>
        public short_url_log GetDataByShortId(string shortid)
        {
            var data = Broker.Retrieve<short_url_log>("select * from short_url_log where short_key = @short_key", new Dictionary<string, object>() { { "@short_key", shortid } });
            return data;
        }
    }
}