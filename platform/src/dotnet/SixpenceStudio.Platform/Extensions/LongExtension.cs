#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/20 22:17:30
Description：Long类型扩展类
********************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Utils
{
    public static class LongExtension
    {
        public static string ToDateTimeString(this long value, string format = "yyyy-MM-dd HH:mm")
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(value * 1000).ToLocalTime().ToString(format);
        }

        public static DateTime ToDateTime(this long value)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(value * 1000).ToLocalTime();
        }
    }
}
