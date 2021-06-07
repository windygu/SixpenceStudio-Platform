
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.Core.MessageRemind
{
    [SystemEntity]
    [EntityName("message_remind")]
    public partial class message_remind : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string message_remindId
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
        /// 关联实体id
        /// </summary>
        private string _object_id;
        [DataMember]
        public string object_id
        {
            get
            {
                return this._object_id;
            }
            set
            {
                this._object_id = value;
                SetAttributeValue("object_id", value);
            }
        }


        /// <summary>
        /// 关联实体name
        /// </summary>
        private string _object_idName;
        [DataMember]
        public string object_idName
        {
            get
            {
                return this._object_idName;
            }
            set
            {
                this._object_idName = value;
                SetAttributeValue("object_idName", value);
            }
        }


        /// <summary>
        /// 实体
        /// </summary>
        private string _object_type;
        [DataMember]
        public string object_type
        {
            get
            {
                return this._object_type;
            }
            set
            {
                this._object_type = value;
                SetAttributeValue("object_type", value);
            }
        }


        /// <summary>
        /// 实体名
        /// </summary>
        private string _object_typeName;
        [DataMember]
        public string object_typeName
        {
            get
            {
                return this._object_typeName;
            }
            set
            {
                this._object_typeName = value;
                SetAttributeValue("object_typeName", value);
            }
        }


        /// <summary>
        /// 是否阅读
        /// </summary>
        private bool _is_read;
        [DataMember]
        public bool is_read
        {
            get
            {
                return this._is_read;
            }
            set
            {
                this._is_read = value;
                SetAttributeValue("is_read", value);
            }
        }


        /// <summary>
        /// 是否阅读
        /// </summary>
        private string _is_readName;
        [DataMember]
        public string is_readName
        {
            get
            {
                return this._is_readName;
            }
            set
            {
                this._is_readName = value;
                SetAttributeValue("is_readName", value);
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

    }
}

