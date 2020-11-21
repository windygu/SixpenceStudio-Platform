using SixpenceStudio.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Utils
{
    public static class ExceptionUtil
    {
        public static void CheckBoolean<T>(bool result, string errorMessage, string errorId)
            where T : Exception
        {
            if (result)
            {
                var ex = Activator.CreateInstance(typeof(T), errorMessage) as T;
                if (ex != null)
                {
                    LogUtils.Error($"{errorId}：{errorMessage}");
                    throw ex;
                }
            }
        }
    }
}
