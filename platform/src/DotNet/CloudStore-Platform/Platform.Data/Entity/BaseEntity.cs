using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Data.Entity
{
    public abstract class BaseEntity
    {
        #region 构造函数
        protected BaseEntity() { }

        protected BaseEntity(string EntityName)
        {
            this._entityName = EntityName;
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
                    if (Attributes.ContainsKey(EntityName + "Id") && Attributes[EntityName + "Id"] !== null)
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
}
