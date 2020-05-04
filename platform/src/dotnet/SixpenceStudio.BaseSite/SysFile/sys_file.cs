using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.BaseSite.SysFile
{
    public class sys_file : BaseEntity
    {
        public sys_file()
        {
            this.EntityName = "sys_file";
        }

        private string _sys_fileid;
        [DataMember]
        public string sys_fileId
        {
            get
            {
                return this._sys_fileid;
            }
            set
            {
                this._sys_fileid = value;
                SetAttributeValue("sys_fileId", value);
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
                SetAttributeValue("CreatedBy", value);
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
                SetAttributeValue("CreatedByName", value);
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
                SetAttributeValue("CreatedOn", value);
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
                SetAttributeValue("ModifiedBy", value);
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
                SetAttributeValue("ModifiedByName", value);
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
                SetAttributeValue("ModifiedOn", value);
            }
        }

    }
}