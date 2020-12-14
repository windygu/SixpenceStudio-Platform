using SixpenceStudio.Core.Configs;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.IoC;
using SixpenceStudio.Core.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.SysFile
{
    public class SysFilePlugin : IPersistBrokerPlugin
    {
        public void Execute(Context context)
        {
            var data = context.Entity as sys_file;
            switch (context.Action)
            {
                case EntityAction.PreCreate:
                    break;
                case EntityAction.PostCreate:
                    break;
                case EntityAction.PreUpdate:
                    break;
                case EntityAction.PostUpdate:
                    break;
                case EntityAction.PreDelete:
                    break;
                case EntityAction.PostDelete:
                    var dataList = context.Broker.RetrieveMultiple<sys_file>(@"
SELECT
	* 
FROM
	sys_file 
WHERE
	hash_code = @hash_code 
	AND sys_fileid <> @Id
", new Dictionary<string, object>() { { "@hash_code", data.hash_code }, { "@id", data.Id } });
                    if (dataList == null || dataList.Count == 0)
                    {
                        var config = ConfigFactory.GetConfig<StoreSection>();
                        UnityContainerService.Resolve<IStoreStrategy>(config?.type).Delete(new List<string>() { data.name });
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
