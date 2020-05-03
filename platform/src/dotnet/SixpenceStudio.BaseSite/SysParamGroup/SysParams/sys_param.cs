using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.BaseSite.SysParams
{
    public partial class sys_param : BaseEntity
    {
        #region 构造函数
        public sys_param()
        {
            this.EntityName = "sys_param";
        }
        #endregion

        /// <summary>
        /// 实体id
        /// </summary>
        private string _sys_paramId;
        [DataMember]
        public string sys_paramId
        {
            get
            {
                return this._sys_paramId;
            }
            set
            {
                this._sys_paramId = value;
                SetAttributeValue("sys_paramId", value);
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        private string _code;
        [DataMember]
        public string code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
                SetAttributeValue("code", value);
            }
        }

        private string _sys_paramGroupId;
        [DataMember]
        public string sys_paramGroupId
        {
            get
            {
                return this._sys_paramGroupId;
            }
            set
            {
                this._sys_paramGroupId = value;
                SetAttributeValue("sys_paramGroupId", value);
            }
        }

        private string _sys_paramGroupIdName;
        [DataMember]
        public string sys_paramGroupIdName
        {
            get
            {
                return this._sys_paramGroupIdName;
            }
            set
            {
                this._sys_paramGroupIdName = value;
                SetAttributeValue("sys_paramGroupIdName", value);
            }
        }

    }
}