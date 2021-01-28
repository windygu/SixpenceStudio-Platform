using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SixpenceStudio.Core.Auth
{
    public class ApplicationContext : ILogicalThreadAffinative
    {
        public const string ContextKey = "Sixpence.ApplicationContext";
        public static ApplicationContext Current
        {
            get
            {
                if (HttpContext.Current?.Session != null)
                {
                    if (HttpContext.Current.Session[ContextKey] == null)
                    {
                        HttpContext.Current.Session[ContextKey] = new ApplicationContext();
                    }

                    return HttpContext.Current.Session[ContextKey] as ApplicationContext;
                }

                if (CallContext.GetData(ContextKey) == null)
                {
                    CallContext.LogicalSetData(ContextKey, new ApplicationContext());
                }

                return CallContext.GetData(ContextKey) as ApplicationContext;
            }
        }

        public CurrentUserModel User;
    }
}
