using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SixpenceStudio.Core.SysParamGroup
{
    public class SysParamGroupService : EntityService<sys_paramgroup>
    {
        #region 构造函数
        public SysParamGroupService()
        {
            this._cmd = new EntityCommand<sys_paramgroup>();
        }
        public SysParamGroupService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<sys_paramgroup>(broker);
        }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            var sql = @"
SELECT
	*
FROM
	sys_paramgroup
";
            var customFilter = new List<string>() { "name" };
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = sql,
                    CustomFilter = customFilter,
                    OrderBy = "name, createdon",
                    ViewId = "457CA7F7-BE57-4934-9434-3234EAF68E14",
                    Name = "所有的选项集"
                }
            };
        }

        public IEnumerable<SelectModel> GetParams(string code)
        {
            var sql = @"
SELECT 
	sys_param.code AS Value,
	sys_param.name AS Name
FROM sys_param
INNER JOIN sys_paramgroup ON sys_param.sys_paramgroupid = sys_paramgroup.sys_paramgroupid
WHERE sys_paramgroup.code = @code
";
            return _cmd.Broker.DbClient.Query<SelectModel>(sql, new Dictionary<string, object>() { { "@code", code } }).ToList();
        }

        public IEnumerable<IEnumerable<SelectModel>> GetParamsList(string[] paramsList)
        {
            var dataList = new List<List<SelectModel>>();
            return paramsList.Select(item => GetParams(item));
        }

        public override void DeleteData(List<string> ids)
        {
            _cmd.Broker.ExecuteTransaction(() =>
            {
                var sql = @"
DELETE FROM sys_param WHERE sys_paramgroupid IN (in@ids)
";
                _cmd.Broker.Execute(sql, new Dictionary<string, object>() { { "in@ids", ids } });
                base.DeleteData(ids);
            });
        }
    }
}