using SixpenceStudio.Core.Store;
using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.IoC;

namespace SixpenceStudio.Core.SysFile
{
    public class SysFileService : EntityService<sys_file>
    {
        #region 构造函数
        public SysFileService()
        {
            this._cmd = new EntityCommand<sys_file>();
        }

        public SysFileService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<sys_file>(broker);
        }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Name = "所有文件",
                    ViewId = "DD1D72FB-D7DE-49AC-B387-273375E6A7BA",
                    Sql = @"
SELECT
	sys_fileid,
	NAME,
	content_type,
	createdon,
	createdbyname
FROM
	sys_file
",
                    OrderBy = "createdon desc",
                    CustomFilter = new List<string>(){ "name" }
                },
                new EntityView()
                {
                    Name = "图库",
                    ViewId = "3BCF6C07-2B49-4D69-9EB1-A3D5B721C976",
                    Sql = @"
SELECT
	sys_fileid,
	NAME,
	createdon,
	createdbyname,
	concat('/api/SysFile/Download?objectId=', sys_fileid) AS downloadUrl
FROM
	sys_file
",
                    OrderBy = "",
                    CustomFilter = new List<string>(){ "name" }
                }
            };
        }

        public IList<sys_file> GetDattaByCode(string code)
        {
            var sql = @"
SELECT * FROM sys_file
WHERE hash_code = @code
";
            return _cmd.Broker.RetrieveMultiple<sys_file>(sql, new Dictionary<string, object>() { { "@code", code } });
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="objectId"></param>
        public void Download(string objectId)
        {
            var config = ConfigFactory.GetConfig<StoreSection>();
            UnityContainerService.Resolve<IStoreStrategy>(config?.type).DownLoad(objectId);
        }

    }
}