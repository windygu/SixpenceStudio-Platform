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