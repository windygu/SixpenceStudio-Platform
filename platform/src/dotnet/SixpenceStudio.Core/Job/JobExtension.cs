using Quartz;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Job
{
    public static class JobExtension
    {
        public static SelectModel ToSelectModel(this TriggerState triggerState)
        {
            switch (triggerState)
            {
                case TriggerState.Normal:
                    return new SelectModel() { Name = "正常", Value = "0" };
                case TriggerState.Paused:
                    return new SelectModel() { Name = "暂停", Value = "1" };
                case TriggerState.Complete:
                    return new SelectModel() { Name = "完成", Value = "2" };
                case TriggerState.Error:
                    return new SelectModel() { Name = "错误", Value = "3" };
                case TriggerState.Blocked:
                    return new SelectModel() { Name = "阻塞", Value = "4" };
                case TriggerState.None:
                default:
                    return new SelectModel() { Name = "不存在", Value = "-1" };
            }
        }

        public static TriggerState ToTriggerState(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return TriggerState.None;
            }
            return (TriggerState)Convert.ToInt32(value);
        }
    }
}
