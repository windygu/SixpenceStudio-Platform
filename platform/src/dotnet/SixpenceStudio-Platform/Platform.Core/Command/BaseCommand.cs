using Platform.Core.Entity;
using Platform.Core.PersistBroker;
using System.Collections.Generic;

namespace Platform.Core.Command
{
    public abstract class BaseCommand
    {
        #region 构造函数
        protected BaseCommand() { }
        protected BaseCommand(IPersistBroker broker)
        {
            _broker = broker;
        }
        #endregion

        #region Persist Broker
        private IPersistBroker _broker;
        public IPersistBroker Broker
        {
            get
            {
                return _broker ?? (_broker = PersistBrokerFactory.GetPersistBroker());
            }
            protected set
            {
                this._broker = value;
            }
        }
        #endregion

        #region Get Entity
        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetEntity<T>(string id)
            where T : BaseEntity, new()
        {
            if (!string.IsNullOrEmpty(id))
                return Broker.Retrieve<T>(id);
            else
                return null;
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Create<T>(T obj)
            where T : BaseEntity, new()
        {
            var id = obj.Id;
            if (GetEntity<T>(id) != null)
            {
                throw new CSException("", "实体已存在，不能重复创建");
            }
            id = Broker.Create(obj);

            return id;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Update<T>(T obj)
            where T : BaseEntity, new()
        {
            if (string.IsNullOrEmpty(obj.Id))
            {
                throw new CSException("", "实体id不能为空");
            }
            Broker.Update(obj);
        }

        /// <summary>
        /// 创建或更新记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string CreateOrUpdateData<T>(T obj)
            where T : BaseEntity, new()
        {
            var id = obj.Id;
            var isExist = GetEntity<T>(id) != null;
            if (isExist)
            {
                Broker.Update(obj);
            }
            else
            {
                Broker.Create(obj);
            }
            return id;
        }

        /// <summary>
        /// 批量删除实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="idList"></param>
        public void Delete<T>(List<string> idList)
            where T : BaseEntity, new()
        {
            if (idList == null || idList.Count == 0)
            {
                throw new CSException("", "请选择实体记录进行删除操作");
            }

            idList.ForEach((id) => Broker.Delete(new T().EntityName, id));
        }


        #endregion
    }
}
