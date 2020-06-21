using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.BaseSite.Job
{
    public class job : BaseEntity
    {
        public job()
        {
            this.EntityName = "job"; 
        }

        /// <summary>
        /// 实体id
        /// </summary>
        private string _jobid;
        [DataMember]
        public string jobId
        {
            get
            {
                return _jobid;
            }
            set
            {
                _jobid = value;
                SetAttributeValue("jobId", value);
            }
        }

       
    }
}