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

        /// <summary>
        /// 执行job
        /// </summary>
        /// <param name="name"></param>
        [HttpPost, HttpGet]
        public void StartJob(string name)
        {
            new JobService().StartJob(name);
        }

        /// <summary>
        /// 暂停 job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        [HttpGet]
        public void PauseJob(string name, string group)
        {
            new JobService().PauseJob(name, group);
        }

        /// <summary>
        /// 删除 job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public void Delete(string name, string group)
        {
            new JobService().DeleteJob(name, group);
        }
    }
}