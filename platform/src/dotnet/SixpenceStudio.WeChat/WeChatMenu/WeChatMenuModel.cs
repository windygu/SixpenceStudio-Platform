using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.WeChat.WeChatMenu
{
    public class WeChatMenuModel
    {
        public List<WeChatMenuButtonModel> button { get; set; }
    }

    public class WeChatMenuButtonModel
    {
        public string type { get; set; }
        public string name { get; set; }

        /// <summary>
        ///类型为click必须要key
        /// </summary>
        public string key { get; set; }
        public string url { get; set; }

        public List<WeChatMenuButtonModel> sub_button { get; set; }
    }
}
