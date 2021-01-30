﻿using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.Core.SysMenu
{
    [EntityName("sys_menu")]
    public partial class sys_menu : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        public string sys_menuId
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
        /// 上级菜单
        /// </summary>
        private string _parentid;
        [DataMember]
        public string parentid
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
                SetAttributeValue("parentid", value);
            }
        }

        /// <summary>
        /// 上级菜单
        /// </summary>
        private string _parentidname;
        [DataMember]
        public string parentIdName
        {
            get
            {
                return this._parentidname;
            }
            set
            {
                this._parentidname = value;
                SetAttributeValue("parentidname", value);
            }
        }

        /// <summary>
        /// 路由地址
        /// </summary>
        private string _router;
        [DataMember]
        public string router
        {
            get
            {
                return this._router;
            }
            set
            {
                this._router = value;
                SetAttributeValue("Router", value);
            }
        }

        private int _menu_index;
        [DataMember]
        public int menu_Index
        {
            get
            {
                return this._menu_index;
            }
            set
            {
                this._menu_index = value;
                SetAttributeValue("Menu_Index", value);
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
        /// 状态
        /// </summary>
        private int? _statecode;
        [DataMember]
        public int? stateCode
        {
            get
            {
                return this._statecode;
            }
            set
            {
                this._statecode = value;
                SetAttributeValue("stateCode", value);
            }
        }

        private string _statecodename;
        [DataMember]
        public string stateCodeName
        {
            get
            {
                return this._statecodename;
            }
            set
            {
                this._statecodename = value;
                SetAttributeValue("stateCodeName", value);
            }
        }

        /// <summary>
        /// 图标
        /// </summary>
        private string _icon;
        [DataMember]
        public string icon
        {
            get
            {
                return this._icon;
            }
            set
            {
                this._icon = value;
                SetAttributeValue("icon", value);
            }
        }
    }
}