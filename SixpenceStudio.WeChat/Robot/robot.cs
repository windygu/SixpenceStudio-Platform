
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.WeChat.Robot
{
    [EntityName("robot", "机器人")]
    public partial class robot : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string robotId
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
        private string _robot_typeName;
        [DataMember]
        public string robot_typeName
        {
            get
            {
                return this._robot_typeName;
            }
            set
            {
                this._robot_typeName = value;
                SetAttributeValue("robot_typeName", value);
            }
        }

    }
}

