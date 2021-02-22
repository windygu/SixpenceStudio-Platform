using Quartz.Impl.AdoJobStore;
using Quartz.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Core.Entity
{
    [DataContract]
    [Serializable]
    public abstract class BaseEntity
    {
        private static readonly ConcurrentDictionary<string, string> _entityNameCache = new ConcurrentDictionary<string, string>();
        public BaseEntity() { }
        public BaseEntity(string entityName) { this._entityName = entityName; }

        /// <summary>
        /// 实体名
        /// </summary>
        private string _entityName;
        public string EntityName
        {
            get
            {
                if (string.IsNullOrEmpty(_entityName))
                {
                    var type = GetType();
                    _entityName = _entityNameCache.GetOrAdd(type.FullName, (key) =>
                    {
                        var attr = Attribute.GetCustomAttribute(type, typeof(EntityNameAttribute)) as EntityNameAttribute;
                        if (attr == null)
                        {
                            throw new SixpenceStudio.Core.SpException("获取实体名失败，请检查是否定义实体名", "");
                        }
                        return attr.Name;
                    });
                }
                return _entityName;
            }
            set
            {
                _entityName = value;
            }
        }

        /// <summary>
        /// 主键名
        /// </summary>
        private string _mainKeyName;
        public string MainKeyName { get { return this._mainKeyName ?? EntityName + "id"; } set { _mainKeyName = value; } }

        /// <summary>
        ///  实体id
        /// </summary>
        private string _id;
        [DataMember]
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
                SetAttributeValue($"{EntityName}Id", value);
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        private string _name;
        [DataMember]
        public string name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                SetAttributeValue("name", value);
            }
        }

        /// <summary>
        /// 实体属性
        /// </summary>
        private readonly Dictionary<string, object> _attributes = new Dictionary<string, object>();

        public Dictionary<string, object> Attributes => _attributes;

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get => GetAttributeValue(key);
            set => SetAttributeValue(key, value);
        }

        #region Methods
        /// <summary>
        ///获取属性字段值
        /// </summary>
        /// <param name="attributeLogicalName">字段名称</param>
        public object GetAttributeValue(string attributeLogicalName)
        {
            return _attributes.ContainsKey(attributeLogicalName)
                    ? _attributes[attributeLogicalName]
                    : null;
        }

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
