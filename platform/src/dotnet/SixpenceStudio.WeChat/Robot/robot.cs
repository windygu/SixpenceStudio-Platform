
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.WeChat.Robot
{
    [EntityName("robot")]
    public partial class robot : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _robotid;
        [DataMember]
        public string robotId
        {
            get
            {
                return this._robotid;
            }
            set
            {
                this._robotid = value;
                SetAttributeValue("robotId", value);
            }
        }

        
        /// <summary>
        /// 钩子地址
        /// </summary>
        private string _hook;
        [DataMember]
        public string hook
        {
            get
            {
                return this._hook;
            }
            set
            {
                this._hook = value;
                SetAttributeValue("hook", value);
            }
        }


        /// <summary>
        /// 说明
        /// </summary>
        private string _description;
        [DataMember]
        public string description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
                SetAttributeValue("description", value);
            }
        }


        /// <summary>
        /// 类型
        /// </summary>
        private string _robot_type;
        [DataMember]
        public string robot_type
        {
            get
            {
                return this._robot_type;
            }
            set
            {
                this._robot_type = value;
                SetAttributeValue("robot_type", value);
            }
        }


        /// <summary>
        /// 类型名称
        /// </summary>
        private string _robot_typename;
        [DataMember]
        public string robot_typename
        {
            get
            {
                return this._robot_typename;
            }
            set
            {
                this._robot_typename = value;
                SetAttributeValue("robot_typename", value);
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

