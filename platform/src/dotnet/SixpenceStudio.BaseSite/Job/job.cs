using SixpenceStudio.Platform.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.BaseSite.Job
{
    [EntityName("job")]
    public class job : BaseEntity
    {
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

        /// <summary>
        /// 执行计划
        /// </summary>
        private string _runtime;
        [DataMember]
        public string runTime
        {
            get
            {
                return _runtime;
            }
            set
            {
                _runtime = value;
                SetAttributeValue("runTime", value);
            }
        }

        /// <summary>
        /// 上次运行时间
        /// </summary>
        private DateTime? _lastRunTime;
        [DataMember]
        public DateTime? lastRunTime
        {
            get
            {
                return _lastRunTime;
            }
            set
            {
                _lastRunTime = value;
                SetAttributeValue("lastRunTime", value);
            }
        }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        private DateTime? _nextRunTime;
        [DataMember]
        public DateTime? nextRunTime
        {
            get
            {
                return _nextRunTime;
            }
            set
            {
                _nextRunTime = value;
                SetAttributeValue("nextRunTime", value);
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string _description;
        [DataMember]
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                SetAttributeValue("description", value);
            }
        }
    }
}