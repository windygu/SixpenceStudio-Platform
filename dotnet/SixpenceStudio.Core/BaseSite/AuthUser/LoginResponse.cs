using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.AuthUser
{
    /// <summary>
    /// 登录返回结果
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// 结果
        /// </summary>
        public bool result { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Ticket { get; set; }
    }
}
