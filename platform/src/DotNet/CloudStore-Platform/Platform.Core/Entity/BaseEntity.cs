using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Platform.Core.Entity
{
    /// <summary>
    /// 实体基类
    /// </summary>
    [DataContract]
    [Serializable]
    public abstract class BaseEntity
    {
        #region 构造函数
        protected BaseEntity() { }

        protected BaseEntity(string EntityName)
        {
            this._entityName = EntityName;
        }

        protected BaseEntity(string EntityName, string Id)
        {
            this._entityName = EntityName;
            this.Id = Id;
        }
        #endregion

        #region 实体名
        /// <summary>
        /// 实体名
        /// </summary>
        private string _entityName;

        public string EntityName
        {
            get
            {
                return _entityName;
            }
            set
            {
                this._entityName = value;
            }
        }

        /// <summary>
        /// 主键属性
        /// </summary>
        public PropertyInfo GetKeyProperty()
        {
            if (_keyProperty == null)
            {
                var type = GetType();
                _keyProperty = type.GetProperties()
                    .FirstOrDefault(p => p.IsDefined(typeof(KeyAttributeLogicalNameAttribute), true));
            }
            return _keyProperty;
        }

        /// <summary>
        /// 获取实体名
        /// </summary>
        /// <returns></returns>
        public string GetEntityName()
        {
            return this.EntityName;
        }
        #endregion

        #region 实体id
        private string _id;

        public string Id
        {
            get
            {
                if (_id == null)
                {
                    if (Attributes.ContainsKey(EntityName + "Id") && Attributes[EntityName + "Id"] != null)
                    {
                        _id = Attributes[EntityName + "Id"].ToString();
                    }
                }
                return _id;
            }
            set
            {
                _id = value;

                SetAttributeValue(EntityName + "Id", value);
            }
        }
        #endregion

        #region 实体数据
        /// <summary>
        /// 实体数据
        /// </summary>
        public Dictionary<string, object> Attributes => _attributes;

        private readonly Dictionary<string, object> _attributes = new Dictionary<string, object>();

        /// <summary>
        /// 获取属性字段值
        /// </summary>
        /// <param name="attributeLogicalName">字段名称</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T GetAttributeValue<T>(string attributeLogicalName) where T : class
        {
            if (_attributes.ContainsKey(attributeLogicalName))
            {
                return _attributes[attributeLogicalName] as T;
            }
            return null;
        }

        /// <summary>
        /// 给字段赋值
        /// </summary>
        /// <param name="attributeLogicalName"></param>
        /// <param name="value"></param>
        public void SetAttributeValue(string attributeLogicalName, object value)
        {
            _attributes[attributeLogicalName] = value;
        }
        #endregion
    }

    /// <summary>
    /// 使用后期绑定的动态实体类
    /// </summary>
    [Serializable]
    public sealed class XrmEntity : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XrmEntity"/> class.
        /// </summary>
        public XrmEntity()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logicalName">实体名称</param>
        public XrmEntity(string logicalName)
            : base(logicalName)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logicalName">实体名称</param>
        /// <param name="id">实体id</param>
        public XrmEntity(string logicalName, string id)
            : base(logicalName, id)
        {
        }
    }
}
