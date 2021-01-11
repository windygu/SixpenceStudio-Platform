
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.WeChat.RobotMessageTask
{
    [EntityName("robot_message_task")]
    public partial class robot_message_task : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _robot_message_taskid;
        [DataMember]
        public string robot_message_taskId
        {
            get
            {
                return this._robot_message_taskid;
            }
            set
            {
                this._robot_message_taskid = value;
                SetAttributeValue("robot_message_taskId", value);
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
        /// 创建人
        /// </summary>
        private string _createdBy;
        [DataMember]
        public string createdBy
        {
            get
            {
                return this._createdBy;
            }
            set
            {
                this._createdBy = value;
                SetAttributeValue("createdBy", value);
            }
        }


        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdByName;
        [DataMember]
        public string createdByName
        {
            get
            {
                return this._createdByName;
            }
            set
            {
                this._createdByName = value;
                SetAttributeValue("createdByName", value);
            }
        }


        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime? _createdOn;
        [DataMember]
        public DateTime? createdOn
        {
            get
            {
                return this._createdOn;
            }
            set
            {
                this._createdOn = value;
                SetAttributeValue("createdOn", value);
            }
        }


        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedBy;
        [DataMember]
        public string modifiedBy
        {
            get
            {
                return this._modifiedBy;
            }
            set
            {
                this._modifiedBy = value;
                SetAttributeValue("modifiedBy", value);
            }
        }


        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedByName;
        [DataMember]
        public string modifiedByName
        {
            get
            {
                return this._modifiedByName;
            }
            set
            {
                this._modifiedByName = value;
                SetAttributeValue("modifiedByName", value);
            }
        }


        /// <summary>
        /// 修改日期
        /// </summary>
        private DateTime? _modifiedOn;
        [DataMember]
        public DateTime? modifiedOn
        {
            get
            {
                return this._modifiedOn;
            }
            set
            {
                this._modifiedOn = value;
                SetAttributeValue("modifiedOn", value);
            }
        }


    }
}

