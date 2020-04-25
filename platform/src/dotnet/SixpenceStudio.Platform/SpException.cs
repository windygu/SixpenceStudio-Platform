using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform
{
    public class SpException : ApplicationException
    {
        /// <summary>
        /// 错误提示
        /// </summary>
        private string message;
        public override string Message => message;

        /// <summary>
        /// 错误提示的 Id
        /// </summary>
        private string messageId;
        public string MessageId => messageId;

        /// <summary>
        /// 错误代码
        /// </summary>
        private string errorCode;
        public string ErrorCode => errorCode;

        public SpException(string message, string messageId)
        {
            this.message = message;
            this.messageId = messageId;
        }

    }
}
