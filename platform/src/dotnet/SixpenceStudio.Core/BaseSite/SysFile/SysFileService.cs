﻿using SixpenceStudio.Core.Store;
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


        public IList<sys_file> GetDattaByCode(string code)
        {
            var sql = @"
SELECT * FROM sys_file
WHERE hash_code = @code
";
            return _cmd.Broker.RetrieveMultiple<sys_file>(sql, new Dictionary<string, object>() { { "@code", code } });
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="ids"></param>
        public override void DeleteData(List<string> ids)
        {
            _cmd.Broker.ExecuteTransaction(() =>
            {
                ids.ForEach(item =>
                {
                    var data = GetData(item);
                    var sql = @"
SELECT COUNT(1) FROM sys_file WHERE hash_code = @code
";
                    var result = _cmd.Broker.ExecuteScalar(sql, new Dictionary<string, object>() { { "@code", data.hash_code } });
                    // 只有当前记录拥有该文件则删除
                    if (Convert.ToInt32(result) <= 1)
                    {
                        FileUtil.DeleteFile(data.file_path);
                    }
                });
                base.DeleteData(ids);
            });
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="objectId"></param>
        public void Download(string objectId)
        {
            var config = ConfigFactory.GetConfig<StoreSection>();
            AssemblyUtil.GetObject<IStoreStrategy>(config?.type).DownLoad(objectId);
        }

    }
}