using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.SysParams
{
    public class SysParamInitialDataProvider : IEntityInitialDataProvider
    {
        public string EntityName => "sys_param";

        public IEnumerable<BaseEntity> GetInitialData()
        {
            return new List<sys_param>()
            {
                new sys_param() { Id = "EC95AF46-41AD-4DB7-9CA8-31BB370DCE90", code = "0", name = "正常", sys_paramGroupId = "E7D80743-081D-4B51-BFFF-149FFFF8E652", sys_paramGroupIdName = "任务状态" },
                new sys_param() { Id = "7963E073-C4B7-4293-B5B7-511A7D4C85AE", code = "1", name = "暂停", sys_paramGroupId = "E7D80743-081D-4B51-BFFF-149FFFF8E652", sys_paramGroupIdName = "任务状态" },
                new sys_param() { Id = "EEF3EEF7-0DDF-4D7C-8067-09F5BF6F29FF", code = "2", name = "完成", sys_paramGroupId = "E7D80743-081D-4B51-BFFF-149FFFF8E652", sys_paramGroupIdName = "任务状态" },
                new sys_param() { Id = "95F893DD-401C-46B7-8BFF-A5D3B64DC25C", code = "3", name = "错误", sys_paramGroupId = "E7D80743-081D-4B51-BFFF-149FFFF8E652", sys_paramGroupIdName = "任务状态" },
                new sys_param() { Id = "A286FD47-A4AF-4DBB-8F2A-11AFD12669B6", code = "4", name = "阻塞", sys_paramGroupId = "E7D80743-081D-4B51-BFFF-149FFFF8E652", sys_paramGroupIdName = "任务状态" },
                new sys_param() { Id = "F76A6F62-7E7B-42B4-8657-195C3841E997", code = "-1", name = "不存在", sys_paramGroupId = "E7D80743-081D-4B51-BFFF-149FFFF8E652", sys_paramGroupIdName = "任务状态" },

                new sys_param() { Id = "D1897BBD-7C79-4796-B27F-58AD02712F74", code = "all", name = "全部", sys_paramGroupId = "CE9753B3-E86F-4DF9-A63D-8B46AD8A187C", sys_paramGroupIdName = "权限" },
                new sys_param() { Id = "A4BC8015-8759-4A52-97F9-B2CF3493C5EB", code = "user", name = "个人", sys_paramGroupId = "CE9753B3-E86F-4DF9-A63D-8B46AD8A187C", sys_paramGroupIdName = "权限" },
                new sys_param() { Id = "981B4FB8-FFBE-4C5D-A49B-3FA58803EA5C", code = "group", name = "分组", sys_paramGroupId = "CE9753B3-E86F-4DF9-A63D-8B46AD8A187C", sys_paramGroupIdName = "权限" },
                new sys_param() { Id = "B1656E3B-DCB5-427A-8A8F-0C10763007B5", code = "guest", name = "游客", sys_paramGroupId = "CE9753B3-E86F-4DF9-A63D-8B46AD8A187C", sys_paramGroupIdName = "权限" },

                new sys_param() { Id = "0ED53489-469B-4CB2-A4F2-61DEBD361952", code = "write", name = "写", sys_paramGroupId = "E944E20B-A463-4FE3-B2E6-ADE32C0709F3", sys_paramGroupIdName = "操作类型" },
                new sys_param() { Id = "F097E80C-119C-4488-A272-6E92EB34C844", code = "read", name = "读", sys_paramGroupId = "E944E20B-A463-4FE3-B2E6-ADE32C0709F3", sys_paramGroupIdName = "操作类型" },
                new sys_param() { Id = "9E817601-9959-4388-9879-4F7086D53343", code = "delete", name = "删除", sys_paramGroupId = "E944E20B-A463-4FE3-B2E6-ADE32C0709F3", sys_paramGroupIdName = "操作类型" },
            };
        }
    }
}
