using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.BaseSite.SysParamGroup
{
    public partial class sys_paramgroup : BaseEntity
    {
        #region 构造函数
        public sys_paramgroup()
        {
            this.EntityName = "sys_paramGroup";
        }
        #endregion

        /// <summary>
        /// 主键
        /// </summary>
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

    }
}