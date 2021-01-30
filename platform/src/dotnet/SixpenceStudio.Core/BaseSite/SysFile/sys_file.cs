using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.Core.SysFile
{
    [EntityName("sys_file")]
    public partial class sys_file : BaseEntity
    {
        [DataMember]
        public string sys_fileId
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
        /// 文件对象
        /// </summary>
        private string _objectid;
        [DataMember]
        public string objectId
        {
            get
            {
                return this._objectid;
            }
            set
            {
                this._objectid = value;
                SetAttributeValue("objectid", value);
            }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        private string _file_path;
        [DataMember]
        public string file_path
        {
            get
            {
                return this._file_path;
            }
            set
            {
                this._file_path = value;
                SetAttributeValue("file_path", value);
            }
        }

        private string _hash_code;
        [DataMember]
        public string hash_code
        {
            get
            {
                return this._hash_code;
            }
            set
            {
                this._hash_code = value;
                SetAttributeValue("hash_code", value);
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdby;
        [DataMember]
        public string createdBy
        {
            get
            {
                return this._createdby;
            }
            set
            {
                this._createdby = value;
                SetAttributeValue("createdBy", value);
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        private string _createdbyname;
        [DataMember]
        public string createdByName
        {
            get
            {
                return this._createdbyname;
            }
            set
            {
                this._createdbyname = value;
                SetAttributeValue("createdByName", value);
            }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime? _createdon;
        [DataMember]
        public DateTime? createdOn
        {
            get
            {
                return this._createdon;
            }
            set
            {
                this._createdon = value;
                SetAttributeValue("createdOn", value);
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedby;
        [DataMember]
        public string modifiedBy
        {
            get
            {
                return this._modifiedby;
            }
            set
            {
                this._modifiedby = value;
                SetAttributeValue("modifiedBy", value);
            }
        }

        /// <summary>
        /// 修改人
        /// </summary>
        private string _modifiedbyname;
        [DataMember]
        public string modifiedByName
        {
            get
            {
                return this._modifiedbyname;
            }
            set
            {
                this._modifiedbyname = value;
                SetAttributeValue("modifiedByName", value);
            }
        }

        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime? _modifiedon;
        [DataMember]
        public DateTime? modifiedOn
        {
            get
            {
                return this._modifiedon;
            }
            set
            {
                this._modifiedon = value;
                SetAttributeValue("modifiedOn", value);
            }
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        private string _file_type;
        [DataMember]
        public string file_type
        {
            get
            {
                return this._file_type;
            }
            set
            {
                this._file_type = value;
                SetAttributeValue("file_type", value);
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        private string _content_type;
        [DataMember]
        public string content_type
        {
            get
            {
                return this._content_type;
            }
            set
            {
                this._content_type = value;
                SetAttributeValue("content_type", value);
            }
        }
    }
}