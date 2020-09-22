using SixpenceStudio.BaseSite.SysFile;
using SixpenceStudio.Platform.Utils;
using System.Runtime.Serialization;

namespace SixpenceStudio.BaseSite.UserInfo
{
    public partial class user_info
    {
        [DataMember]
        public string avatarUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.avatar))
                {
                    return $"/{FileUtil.storage}/" + new SysFileService().GetData(this.avatar).name;
                }
                return "";
            }
        }
    }
}