﻿using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.Core.SysParams
{
    [SystemEntity]
    [EntityName("sys_param")]
    public partial class sys_param : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("sys_paramid", "实体id", AttrType.Varchar, 100)]
        public string sys_paramId
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
        /// 编码
        /// </summary>
        private string _code;
        [DataMember]
        [Attr("code", "编码", AttrType.Varchar, 100)]
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
        [Attr("sys_paramgroupid", "选项集id", AttrType.Varchar, 100)]
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
        [Attr("sys_paramgroupidname", "选项集名", AttrType.Varchar, 100)]
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