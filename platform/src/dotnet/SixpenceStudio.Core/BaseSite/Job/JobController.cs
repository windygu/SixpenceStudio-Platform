using Newtonsoft.Json;
using SixpenceStudio.Core.AuthUser;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SixpenceStudio.Core.Job
{
    public class JobController : BaseController
    {
        [HttpGet]
        public IList<job> GetDataList()
        {
            return new JobService().GetDataList();
        }

        [HttpGet]
        public void RunOnceNow(string name)
        {
            new JobService().RunOnceNow(name);
        }
    }
}