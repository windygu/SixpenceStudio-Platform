using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SixpenceStudio.BaseSite.SysMenu
{
    [EntityName("sys_menu")]
    public partial class sys_menu : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _sys_menuid;
        [DataMember]
        public string sys_menuId
        {
            get
            {
                return this._sys_menuid;
            }
            set
            {
                this._sys_menuid = value;
                SetAttributeValue("sys_menuId", value);
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

    }
}