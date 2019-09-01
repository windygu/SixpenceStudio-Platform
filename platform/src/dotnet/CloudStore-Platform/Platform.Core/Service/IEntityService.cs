using Platform.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Core.Service
{
    public interface IEntityService<E>
        where   E : BaseEntity, new()
    {
        DataListModel<E> GetDataList(string viewId, string queryValue, int pageIndex, int pageSize);

        E GetData(string id);

        string CreateData(E model);

        void UpdateData(E model);

        string CreateOrUpdateData(E model);

        BatchResponse BatchCreateOrUpdate(List<E> modelList);

        void DeleteData(List<string> idList);
    }

    /// <summary>
    /// 用于WebAPI返回列表类型的公用的数据容器类
    /// </summary>
    public class DataListModel<T> where T : class
    {
        /// <summary>
        /// 根据查询SQL语句查询出来的总记录数
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// 查询的结果数据中包含当前页的list
        /// </summary>
        public IList<T> DataList { get; set; }
    }

}
