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
    }
}