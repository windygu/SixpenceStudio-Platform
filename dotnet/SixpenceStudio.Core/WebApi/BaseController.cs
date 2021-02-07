using SixpenceStudio.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Core.WebApi
{
    [Route("api/[controller]/[action]")]
    [WebApiExceptionFilter, WebApiTracker]
    public class BaseController : ApiController
    {

    }
}
