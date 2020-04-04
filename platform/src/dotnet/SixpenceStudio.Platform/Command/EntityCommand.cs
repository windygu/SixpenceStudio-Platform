using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Platform.Command
{
    public class EntityCommand<T>
        where T : BaseEntity, new ()
    {
        #region 构造函数
        public EntityCommand() { }
        public EntityCommand(IPersistBroker broker)
        {
            this._broker = broker;
        }
        #endregion

        private IPersistBroker _broker;
        public IPersistBroker broker
        {
            get
            {
                return _broker ?? new PersistBroker();
            }
            set
            {
                this._broker = value;
            }
        }

        /// <summary>
        ///  获取所有实体记录
        /// </summary>
        /// <returns></returns>
        public IList<T> GetAllEntity()
        {
            var sql = $"SELECT *  FROM {new T().EntityName}";
            var data = broker.RetrieveMultiple<T>(sql);
            return data;
        }

        /// <summary>
        /// 获取实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEntity<T>(string id) where T : BaseEntity, new()
        {
            return broker.Retrieve<T>(id);
        }

        /// <summary>
        /// 创建实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string Create<T>(T t) where T : BaseEntity, new()
        {
            if (string.IsNullOrEmpty(t.Id))
            {
                return "";
            }
            
            if (t.Attributes.ContainsKey("CreatedBy") && t.GetAttributeValue("CreatedBy") == null)
            {
                t.SetAttributeValue("CreatedBy", "");
            }
            return broker.Create(t);
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Update<T>(T t) where T : BaseEntity, new()
        {
            broker.Update(t);
        }

        /// <summary>
        /// 创建或更新历史记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string CreateOrUpdateData<T>(T t) where T : BaseEntity, new()
        {
            var id = t.Id;
            var isExist = GetEntity<T>(id) != null;
            if (isExist)
            {
                broker.Update(t);
            }
            else
            {
                id = broker.Create(t);
            }
            return id;
        }

        /// <summary>
        /// 删除历史记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        public void Delete<T>(List<string> ids) where T : BaseEntity, new()
        {
            ids.ForEach(id => broker.Delete(new T().EntityName, id));
        }

    }
}
