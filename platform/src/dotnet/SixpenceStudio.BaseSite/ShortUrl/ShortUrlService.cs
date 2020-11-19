using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.ShortUrl
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

        public string CreateData(string longUrl)
        {
            var entity = new short_url_log()
            {
                Id = Guid.NewGuid().ToString(),
                long_url = longUrl
            };
            var shortUrl = ShortenUrl.AddUrl(new string[] { longUrl });
            entity.short_url = shortUrl?.FirstOrDefault();
            CreateData(entity);
            return entity.short_url;
        }

        public string GetShortUrl(string id)
        {
            return GetData(id)?.short_url;
        }
    }
}