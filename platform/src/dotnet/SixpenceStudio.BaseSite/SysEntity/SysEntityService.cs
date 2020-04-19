using SixpenceStudio.BaseSite.SysEntity.SysAttrs;
using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.BaseSite.SysEntity
{
    public class SysEntityService : EntityService<sys_entity>
    {
        #region 构造函数
        public SysEntityService()
        {
            _cmd = new EntityCommand<sys_entity>();
        }
        
        public SysEntityService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<sys_entity>(broker);
        }
        #endregion

        /// <summary>
        /// 根据实体 id 查询字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<sys_attrs> GetEntityAttrs(string id)
        {
            var sql = @"
SELECT
	*
FROM 
	sys_attrs
WHERE
	entityid = @id
";
            return _cmd.broker.RetrieveMultiple<sys_attrs>(sql, new Dictionary<string, object>() { { "@id", id } });
        }
    }
}