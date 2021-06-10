
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.WeChat.RobotMessageTask
{
    [EntityName("robot_message_task", "机器人消息任务")]
    public partial class robot_message_task : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string robot_message_taskId
        {
            get
            {
                return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }

        
        /// <summary>
        /// 消息内容
        /// </summary>
        private string _content;
        [DataMember]
        public string content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
                SetAttributeValue("content", value);
            }
        }


        /// <summary>
        /// 执行时间
        /// </summary>
        private string _runtime;
        [DataMember]
        public string runtime
        {
            get
            {
                return this._runtime;
            }
            set
            {
                this._runtime = value;
                SetAttributeValue("runtime", value);
            }
        }


        /// <summary>
        /// 消息类型
        /// </summary>
        private string _message_type;
        [DataMember]
        public string message_type
        {
            get
            {
                return this._message_type;
            }
            set
            {
                this._message_type = value;
                SetAttributeValue("message_type", value);
            }
        }


        /// <summary>
        /// 消息类型名称
        /// </summary>
        private string _message_typeName;
        [DataMember]
        public string message_typeName
        {
            get
            {
                return this._message_typeName;
            }
            set
            {
                this._message_typeName = value;
                SetAttributeValue("message_typeName", value);
            }
        }

        /// <summary>
        /// 机器人
        /// </summary>
        private string _robotid;
        [DataMember]
        public string robotid
        {
            get
            {
                return this._robotid;
            }
            set
            {
                this._robotid = value;
                SetAttributeValue("robotid", value);
            }
        }


        /// <summary>
        /// 机器人名称
        /// </summary>
        private string _robotidName;
        [DataMember]
        public string robotidName
        {
            get
            {
                return this._robotidName;
            }
            set
            {
                this._robotidName = value;
                SetAttributeValue("robotidName", value);
            }
        }


        /// <summary>
        /// 任务状态
        /// </summary>
        private string _job_state;
        [DataMember]
        public string job_state
        {
            get
            {
                return this._job_state;
            }
            set
            {
                this._job_state = value;
                SetAttributeValue("job_state", value);
            }
        }


        /// <summary>
        /// 任务状态名称
        /// </summary>
        private string _job_stateName;
        [DataMember]
        public string job_stateName
        {
            get
            {
                return this._job_stateName;
            }
            set
            {
                this._job_stateName = value;
                SetAttributeValue("job_stateName", value);
            }
        }

    }
}

