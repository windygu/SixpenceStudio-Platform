﻿using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SixpenceStudio.Platform.Service
{
    public class EntityService<T>
        where T : BaseEntity, new()
    {
        protected EntityCommand<T> _cmd;

        protected IPersistBroker Broker
        {
            get
            {
                return _cmd.broker;
            }
        }

        #region 实体表单
        /// <summary>
        /// 获取视图
        /// </summary>
        /// <returns></returns>
        public virtual IList<EntityView<T>> GetViewList()
        {
            var sql = $"SELECT * FROM {new T().EntityName} WHERE 1=1";
            return new List<EntityView<T>>()
            {
                new EntityView<T>()
                {
                    Sql = sql,
                    OrderBy = "",
                    ViewId = ""
                }
            };
        }

        /// <summary>
        /// 获取所有实体记录
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetDataList(IList<SearchCondition> searchList, string orderBy, string viewId = "", string searchValue = "")
        {
            var view = string.IsNullOrEmpty(viewId) ? GetViewList().ToList().FirstOrDefault() : GetViewList().ToList().Find(item => item.ViewId == viewId);
            return _cmd.GetDataList(view, searchList, orderBy);
        }

        /// <summary>
        /// 获取所有实体记录
        /// </summary>
        /// <returns></returns>
        public virtual DataModel<T> GetDataList(IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            var view = string.IsNullOrEmpty(viewId) ? GetViewList().ToList().FirstOrDefault() : GetViewList().ToList().Find(item => item.ViewId == viewId);
            var data = _cmd.GetDataList(view, searchList, orderBy, pageSize, pageIndex, out var recordCount, searchValue);
            return new DataModel<T>()
            {
                DataList = data,
                RecordCount = recordCount
            };
        }

        /// <summary>
        /// 获取实体记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetData(string id)
        {
            var obj = _cmd.GetEntity<T>(id);
            return obj;
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual string CreateData(T t)
        {
            return _cmd.Create(t);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="t"></param>
        public virtual void UpdateData(T t)
        {
            _cmd.Update(t);
        }

        /// <summary>
        /// 创建或更新记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual string CreateOrUpdateData(T t)
        {
            return _cmd.CreateOrUpdateData(t);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ids"></param>
        public virtual void DeleteData(List<string> ids)
        {
            _cmd.Delete<T>(ids);
        }
        #endregion
    }
}
