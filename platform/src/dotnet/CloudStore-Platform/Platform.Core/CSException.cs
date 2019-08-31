using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Core
{
    public class CSException : ApplicationException
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        readonly string _errorMessage;

        /// <summary>
        /// 异常信息的消息Id
        /// </summary>
        public string MessageId { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="msgId">错误Id</param>
        /// <param name="msgBody">错误消息</param>
        public CSException(string msgId, string msgBody)
        {
            MessageId = msgId;
            _errorMessage = msgBody;
        }

        /// <summary>
        /// 异常的详细信息
        /// </summary>
        public override string Message => _errorMessage;
    }
}
