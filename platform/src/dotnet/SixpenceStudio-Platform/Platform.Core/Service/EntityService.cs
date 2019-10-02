using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Core.Command;
using Platform.Core.Entity;

namespace Platform.Core.Service
{
    public class EntityService<E> : IEntityService<E>
        where E : BaseEntity, new()
    {
        protected const string INTERNAL_VIEW_ID = "21EF7C4E-FE46-4E35-9F05-E074947BF0ED";

        /// <summary>
        /// 实体处理逻辑类
        /// </summary>
        protected EntityCommand<E> _cmd;

        public virtual BatchResponse BatchCreateOrUpdate(BatchRequest<E> batchRequest)
        {
            var response = BatchCreateOrUpdate(batchRequest.BatchList);
            return response;
        }

        public virtual BatchResponse BatchCreateOrUpdate(List<E> modelList)
        {
            var batchResponse = new BatchResponse();
            if (modelList == null || modelList.Count <= 0)
            {
                batchResponse.ErrorCode = -1;
                batchResponse.Message = "传入的参数为空";
                return batchResponse;
            }
            modelList.ForEach(m =>
            {
                try
                {
                    var response = CreateOrUpdateData(m);
                    if (!string.IsNullOrWhiteSpace(response))
                    {
                        batchResponse.Data.Add(new BaseResponse<string>()
                        {
                            ErrorCode = 0,
                            Message = string.Empty,
                            Data = response
                        });

                    }
                }
                catch (Exception ex)
                {
                    batchResponse.Data.Add(new BaseResponse<string>()
                    {
                        ErrorCode = -1,
                        Message = $"数据{m.Id}更新失败,失败原因：" + ex.Message,
                        Data = m.Id
                    });
                }
            });
            var hasErrResponse = batchResponse.Data.Where(n => n.ErrorCode < 0);
            if (hasErrResponse != null && hasErrResponse.Count() > 0)
            {
                batchResponse.ErrorCode = -1;
                return batchResponse;
            }
            return batchResponse;

        }

        /// <summary>
        /// 创建实体记录
        /// </summary>
        /// <param name="model">实体数据模型</param>
        /// <returns>实体记录标识</returns>
        public virtual string CreateData(E model)
        {
            return _cmd.Create(model);
        }

        /// <summary>
        /// 创建或更新实体
        /// </summary>  
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual string CreateOrUpdateData(E model)
        {
            var id = model.Id;
            var isExist = this.GetData(id) != null;   //记录是否存在
            if (isExist)
            {
                this.UpdateData(model);
            }
            else
            {
                id = this.CreateData(model);
            }

            return id;
        }

        /// <summary>
        /// 删除实体记录
        /// </summary>
        /// <param name="idList">实体记录标识列表</param>
        public virtual void DeleteData(List<string> idList)
        {
            _cmd.Delete<E>(idList);
        }

        /// <summary>
        /// 根据记录标识获取实体记录
        /// </summary>
        /// <param name="id">记录标识</param>
        /// <returns>实体记录</returns>
        public virtual E GetData(string id)
        {
            var obj = _cmd.GetEntity<E>(id);
            return obj;
        }

        /// <summary>
        /// 获取视图List
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="queryValue"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual DataListModel<E> GetDataList(string viewId, string queryValue, int pageIndex, int pageSize)
        {
            var view = (viewId == InternalView.Id) ? InternalView : ViewList.FirstOrDefault(item => item.Id == viewId);
            if (view == null)
            {
                throw new CSException("", "视图为null");
            }

            int recordCount;
            var list = new EntityCommand<E>(_cmd.Broker).GetEntityViewDataList(view, pageIndex, pageSize, out recordCount, queryValue, null);

            return new DataListModel<E>
            {
                DataList = list.ToList(),
                TotalRecords = recordCount
            };
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <param name="model">实体记录模型</param>
        public virtual void UpdateData(E model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                throw new CSException("", "实体id不能为空");
            }
            _cmd.Update(model);
        }

        #region 实体视图
        /// <summary>
        /// 内置视图，用于批量查询数据
        /// </summary>
        protected virtual EntityViewModel InternalView => new EntityViewModel(INTERNAL_VIEW_ID, "InternalView", $"SELECT * FROM {new E().EntityName}")
        {
            OrderBy = $" ORDER BY ModifiedOn DESC, {new E().EntityName}id"
        };

        /// <summary>
        /// 获取实体的视图列表
        /// </summary>
        /// <value>
        /// 实体的视图列表
        /// </value>
        protected virtual IList<EntityViewModel> ViewList => new List<EntityViewModel>(0);

        /// <summary>
        /// Gets the view list.
        /// </summary>
        /// <param name="viewType">Type of the view.</param>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public virtual IList<EntityViewModel> GetViewList(ViewType viewType, string tag)
        {
            if (ViewList == null)
            {
                return null;
            }

            var viewList = ViewList.Where(view => view.Type == viewType);
            return string.IsNullOrEmpty(tag) ? viewList.Where(v => v.TagList == null || v.TagList.Count == 0).ToList() : viewList.Where(view => view.TagList != null && view.TagList.Contains(tag)).ToList();
        }

        /// <summary>根据参数查询数据</summary>
        /// <param name="searchList">搜索条件</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="pageSize">查询页大小</param>
        /// <param name="pageIndex">查询页索引</param>
        /// <returns></returns>
        public virtual DataListModel<E> GetDataList(List<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex)
        {
            return GetDataList(INTERNAL_VIEW_ID, orderBy, pageIndex, pageSize, null, searchList);
        }

        /// <summary>
        /// 查询查找视图数据
        /// </summary>
        /// <param name="viewId">视图标识</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="quickSearchValue">快速搜索条件</param>
        /// <param name="searchList">The search list.</param>
        /// <returns>
        /// 查找视图数据结果
        /// </returns>
        public virtual DataListModel<E> GetDataList(string viewId, string orderBy, int pageIndex, int pageSize, string quickSearchValue, List<SearchCondition> searchList)
        {
            var view = (viewId == InternalView.Id) ? InternalView : ViewList.FirstOrDefault(item => item.Id == viewId);
            if (view == null)
            {
                throw new CSException("", "视图为null");
            }

            int recordCount;
            var list = new EntityCommand<E>(_cmd.Broker).GetEntityViewDataList(view, pageIndex, pageSize, out recordCount, quickSearchValue, searchList, orderBy);

            return new DataListModel<E>
            {
                DataList = list.ToList(),
                TotalRecords = recordCount
            };
        }

        public virtual DataListModel<E> GetDataList(string viewId, string orderBy, int pageIndex, int pageSize, string quickSearchValue, List<SearchCondition> searchList, List<SearchCondition> bodySearchList)
        {
            searchList.AddRange(bodySearchList);
            return GetDataList(viewId, orderBy, pageIndex, pageSize, quickSearchValue, searchList);
        }

        #endregion
    }
}
